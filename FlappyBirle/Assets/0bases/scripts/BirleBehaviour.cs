using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class BirleBehaviour : MonoBehaviour
{
    [SerializeField]
    private InputActionReference _spaceRed;
    [SerializeField]
    private InputActionReference _clickRef;

    [SerializeField]
    private TextMeshProUGUI _instruccion;
    
    [SerializeField]
    private PalosController _paloManager;
    
    public event Action<int> OnBirleCollision;
    private Rigidbody2D _rigidBody2D;
    private float _velocidad;
    private bool _start = false;

    private void Awake()
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
            _spaceRed.action.performed += Jump;
        }

    private void Jump(InputAction.CallbackContext context)
        {
            if (!_start)
        {
            _start = true;
            _rigidBody2D.WakeUp();
            _rigidBody2D.AddForceY(5, ForceMode2D.Impulse); 
            _instruccion.text = "";
            _paloManager.IniciarPalos(-2f);
        }
        else
        {
            if (_velocidad <= 0)
            {
                _rigidBody2D.AddForceY(8-_velocidad, ForceMode2D.Impulse);
            }
            else
            {
                _rigidBody2D.AddForceY(5, ForceMode2D.Impulse);
            }
        }
            
        }
    
    private void Update()
        {
            _velocidad = _rigidBody2D.linearVelocityY;
        }

    void OnCollisionEnter2D(Collision2D collision)
    {
        _rigidBody2D.Sleep();
        _spaceRed.action.Disable();
        _paloManager.PararPalos();
    }   

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("punto"))
        {
            if (collision.GetComponentInParent<SpriteRenderer>().color == Color.softRed)
            {
                OnBirleCollision?.Invoke(5);
            }
            else
            {
                OnBirleCollision?.Invoke(1);
            }
        }
    }

    
}
