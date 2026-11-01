using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class RolunaBehaviour : MonoBehaviour
{
    [SerializeField]
    private InputActionReference _movimientoRef; 
   
    [SerializeField]
    private InputActionReference _dispararRef; 

    [SerializeField]
    private GameObject _balaPrefab;

    [SerializeField]
    private Transform _puntoDisparo;

    [SerializeField]
    private Transform _limitContacte;

    [SerializeField]
    private Camera _camara;
    private Vector3 _offsetCamara;

    private float _salto = 10f;
    private float _cooldown = 0.20f;
    private float _tiempoActual;
    private Rigidbody2D _rigidBody2D;
    private bool _puedeSaltar = true;
    private Vector2 _inputMovimiento;
    private float _velocidad = 6f;
    private int _ataque = 1;
    private int _vida = 10; 
    private Animator _animator;
    private string _direccion;
    private float _duracionDisparo = 0.333f;
    private float _tiempoDisparo = 0f;
    private string _animActual = "";
    public event Action<int> OnCambioVida;
    private GameManager _gm;

    public int Vida
    {
        get {return _vida;}
        set 
        {
            _vida = value;
            if (_vida < 0)
            {
                _vida = 0;
            };
            OnCambioVida?.Invoke(_vida);
        }
    }
    public float Velocidad
    {
        get {return _velocidad;}
        set {_velocidad = value;}
    }
    public int Ataque
    {
        get {return _ataque;}
        set {_ataque = value;}
    }
    public float Salto
    {
        get {return _salto;}
        set {_salto = value;}
    }
    public float Cooldown
    {
        get {return _cooldown;}
        set 
        {
            _cooldown = value;
            if (_cooldown < 0f)
            {
                _cooldown = 0.01f;
            }
        }
    }

    private void Awake()
    {
        _gm = GameManager.Singleton;
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _tiempoActual = 0;
        _offsetCamara = _camara.transform.localPosition;
    }
    void Start()
    {
        OnCambioVida?.Invoke(_vida);
    }

    private void OnEnable()
    {
        _movimientoRef.action.performed += Saltar;
        _movimientoRef.action.performed += Bajar;
        _dispararRef.action.performed += Disparar;
    }

    private void OnDisable()
    {   
        _movimientoRef.action.performed -= Saltar;
        _movimientoRef.action.performed -= Bajar;
        _dispararRef.action.performed -= Disparar;

    }

    private void Disparar(InputAction.CallbackContext context)
    {
        if(_tiempoActual == 0)
        {
            _tiempoDisparo = _duracionDisparo;
            Vector3 posicionSpawn = _puntoDisparo.position;
            Vector3 direccio = transform.right;
            GameObject nuevaBala = Instantiate(_balaPrefab, posicionSpawn, Quaternion.identity);

            nuevaBala.GetComponent<BalaBehaviour>().Disparar(direccio, _ataque);
            _tiempoActual = _cooldown;
        }
    }


    private void Update()
    {
        _inputMovimiento = _movimientoRef.action.ReadValue<Vector2>();

        if (_tiempoActual <= 0)
        {
            _tiempoActual = 0;
        }
        else
        {
            _tiempoActual -= Time.deltaTime;
        }
        if (_tiempoDisparo > 0f)
        {
            _tiempoDisparo -= Time.deltaTime;
        }
        ActualizarAnimacion();
    }

    private void FixedUpdate()
    {
        if (_inputMovimiento.x < -0.1f)
        {
            _rigidBody2D.linearVelocityX = -_velocidad;
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            _camara.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            _camara.transform.localPosition = new Vector3(-_offsetCamara.x, _offsetCamara.y, -_offsetCamara.z);
        }
        else if (_inputMovimiento.x > 0.1f)
        {
            _rigidBody2D.linearVelocityX = _velocidad;
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            _camara.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            _camara.transform.localPosition = _offsetCamara;
        }
        else
        {
            _rigidBody2D.linearVelocityX = 0;
        }

        if (_rigidBody2D.linearVelocity.y < 0)
        {
            _rigidBody2D.gravityScale = 3f;
        }
        else
        {
            _rigidBody2D.gravityScale = 1.0f;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        ComprobarSuelo(collision);

        if (collision.gameObject.layer == 6)
        {
            Vida = Vida - 1;
            if (Vida <= 0)
            {
                _gm.EndGame();
                return;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ComprobarSuelo(collision);
    }

    private void ComprobarSuelo(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo") && _rigidBody2D.linearVelocity.y <= 0.05f)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.point.y <= _limitContacte.position.y + 0.1f && contact.normal.y > 0.5f)
                {
                    _puedeSaltar = true;
                    break;
                }
            }
        }
    }

    private void Saltar(InputAction.CallbackContext context)
    {
        Vector2 _inputSalto = context.ReadValue<Vector2>();
        if (_inputSalto.y > 0.5f)
        {
            if (_puedeSaltar)
            {
                _puedeSaltar = false;
                _rigidBody2D.linearVelocityY = 0f;
                _rigidBody2D.AddForceY(_salto, ForceMode2D.Impulse);
            }
        }
    }

    private void Bajar(InputAction.CallbackContext context)
    {
        Vector2 _inputSalto = context.ReadValue<Vector2>();
        if (_inputSalto.y < -0.5f && !_puedeSaltar)
        {
            _rigidBody2D.linearVelocityY = 0f;
            _rigidBody2D.AddForceY(-10f, ForceMode2D.Impulse);
        }
    }

    private void ActualizarAnimacion()
    {
        bool _enAire = false;
        if(!_puedeSaltar || _rigidBody2D.linearVelocity.y < -0.5f) _enAire = true;
        bool _corriendo = false;
        if(_inputMovimiento.x < -0.1f || _inputMovimiento.x > 0.1f) _corriendo = true;

        string _anim;
        if (_enAire)
        {
            _anim = "RolunaJump";
        }
        else if (_corriendo)
        {
            _anim = "RolunaRun";
        }
        else
        {
            _anim = "RolunaIdle";
        }

        if (_tiempoDisparo > 0f)
        {
            _anim = _anim + "Shoot";
        }

        if (_anim != _animActual)
        {
            float _inicio = 0f;
            if (_anim.Replace("Shoot", "") == _animActual.Replace("Shoot", ""))
            {
                _inicio = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1f;
            }

            _animator.Play(_anim, 0, _inicio);
            _animActual = _anim;
        }
    }
}
