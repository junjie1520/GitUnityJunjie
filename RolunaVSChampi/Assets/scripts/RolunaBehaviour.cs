using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RolunaBehaviour : MonoBehaviour
{
    [SerializeField]
    private InputActionReference _movimientoRef; 
   

    [SerializeField]
    private float _salto = 6f;

    private Rigidbody2D _rigidBody2D;
    private bool _puedeSaltar = true;
    private Vector2 _inputMovimiento;
    private GameObject _plataformaActual;
    private bool _isBajando = false;
    private float _velocidad = 6f;
    private int _ataque = 1;
    private int _vida = 10;


    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _movimientoRef.action.performed += Saltar;
        _movimientoRef.action.performed += LeerMovimiento;
        _movimientoRef.action.canceled += LeerMovimiento;
    }

    private void OnDisable()
    {   
        _movimientoRef.action.performed -= Saltar;
        _movimientoRef.action.performed -= LeerMovimiento;
        _movimientoRef.action.canceled -= LeerMovimiento;
    }

    private void LeerMovimiento(InputAction.CallbackContext context)
    {
        _inputMovimiento = context.ReadValue<Vector2>();
        if (_inputMovimiento.y < -0.5f && _plataformaActual != null && !_isBajando)
        {
            StartCoroutine(DejarseCaer());
        }
    }

    private void Update()
    {
        if (_inputMovimiento.x < -0.5f){
            transform.Translate(Vector2.left * _velocidad * Time.deltaTime);
        }
        else if (_inputMovimiento.x > 0.5f){
            transform.Translate(Vector2.right * _velocidad * Time.deltaTime);
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
            _vida--;
            Debug.Log("Vidas: " + _vida);
            if (_vida <= 0)
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
