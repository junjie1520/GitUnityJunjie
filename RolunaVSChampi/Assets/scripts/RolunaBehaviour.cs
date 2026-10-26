using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class RolunaBehaviour : MonoBehaviour
{
    [SerializeField]
    private InputActionReference _movimientoRef; 
   
    [SerializeField]
    private InputActionReference _dispararRef; 
   
    [SerializeField]
    private float _salto = 10f;

    [SerializeField]
    private GameObject _balaPrefab;


    [SerializeField]
    private Transform _puntoDisparo;

    private float _coldown = 0.20f;
    private float _tiempoActual;
    private Rigidbody2D _rigidBody2D;
    private bool _puedeSaltar = true;
    private Vector2 _inputMovimiento;
    private GameObject _plataformaActual;
    private bool _isBajando = false;
    private float _velocidad = 6f;
    private int _ataque = 1;
    private int _vida = 10;
    private int _puntos = 0;

    public string _direccion;

    public int getAtaque()
    {
        return _ataque;
    }

    public int getVida()
    {
        return _vida;
    }

    public void setAtaque(int ataque)
    {
        this._ataque = ataque;
    }
    public void setVida(int vida)
    {
        this._vida = vida;
    }

    public int getPuntos()
    {
        return _puntos;
    }

    public void setPuntos(int puntos)
    {
        this._puntos = puntos;
    }

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _tiempoActual = 0;
    }

    private void OnEnable()
    {
        _movimientoRef.action.performed += Saltar;

        _dispararRef.action.performed += Disparar;
    }

    private void OnDisable()
    {   
        _movimientoRef.action.performed -= Saltar;

        _dispararRef.action.performed += Disparar;

    }

    private void Disparar(InputAction.CallbackContext context)
    {
        if(_tiempoActual == 0)
        {
            Vector3 posicionSpawn = _puntoDisparo.position;
            Vector3 direccio = transform.right;
            /*
            if (posicionSpawn.x < transform.position.x)
            {
                direccio = Vector3.left;
            }
            */
            GameObject nuevaBala = Instantiate(_balaPrefab, posicionSpawn, Quaternion.identity);

            nuevaBala.GetComponent<BalaBehaviour>().Disparar(direccio, 15);
            _tiempoActual = _coldown;
        }

    }


    private void Update()
    {
        _inputMovimiento = _movimientoRef.action.ReadValue<Vector2>();
        if(_tiempoActual <= 0)
        {
            _tiempoActual = 0;
        }
        else
        {
            _tiempoActual-=Time.deltaTime;

        }
        if (_inputMovimiento.y < -0.5f && _plataformaActual != null && !_isBajando)
        {
            StartCoroutine(DejarseCaer());
        }

        if (_inputMovimiento.x < -0.5f)
        {
            _rigidBody2D.linearVelocityX = -_velocidad; 
            transform.localEulerAngles = Vector3.up * 180;      
        }
        else if (_inputMovimiento.x > 0.5f){
            _rigidBody2D.linearVelocityX = _velocidad;
            transform.localEulerAngles = Vector3.zero;      

        }
        else
        {
            _rigidBody2D.linearVelocityX = 0;
        }

    }

    private void Saltar(InputAction.CallbackContext context)
    {
        Vector2 _inputSalto = context.ReadValue<Vector2>();
        if (_inputSalto.y > 0.5f){
            if (_puedeSaltar){
                _rigidBody2D.AddForceY(_salto, ForceMode2D.Impulse);
                _puedeSaltar = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo") || collision.gameObject.CompareTag("Plataforma")){
            if (_rigidBody2D.linearVelocity.y <= 0.1f){
                _puedeSaltar = true;
                if (collision.gameObject.CompareTag("Plataforma")){
                    _plataformaActual = collision.gameObject;
                }
            }
        }else if (collision.gameObject.CompareTag("enemigo")){
            setVida(getVida()-1);
            Debug.Log("Vidas: " + _vida);
            if (getVida() <= 0)
            {
                muerte();
            }
        }
    }

    
    private void muerte(){
        Debug.Log("Murio");
    }

    private void OnCollisionExit2D(Collision2D collision)   
    {
        if (collision.gameObject == _plataformaActual){
            _plataformaActual = null;
        }
    }

    private IEnumerator DejarseCaer()
    {
        _isBajando = true;
        PlatformEffector2D effector = _plataformaActual.GetComponent<PlatformEffector2D>();
        if (effector != null)
        {
            effector.rotationalOffset = 180f;
            yield return new WaitForSeconds(0.5f);
            effector.rotationalOffset = 0f;
        }
        _isBajando = false;
    }

    private void FixedUpdate()
    {
        if (_rigidBody2D.linearVelocity.y < 0)
        {
            _rigidBody2D.gravityScale = 3f;
        }
        else
        {
            _rigidBody2D.gravityScale = 1.0f;
        }
    }

    private void atacar(){

    }
}
