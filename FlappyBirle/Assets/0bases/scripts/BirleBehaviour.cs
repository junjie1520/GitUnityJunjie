using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class BirleBehaviour : MonoBehaviour
{
    [SerializeField]
        private InputActionReference _buttonRef;
        
    [SerializeField]
        private TextMeshProUGUI _texto;

    [SerializeField]
        private TextMeshProUGUI _instruccion;
    
    private Rigidbody2D _rigidBody2D;
    private float _velocidad;
    private bool _start = false;
    private int _puntuacion = 80;

    private void Awake()
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
            _buttonRef.action.performed += Jump;
        }

    private void Jump(InputAction.CallbackContext context)
        {
            if (!_start)
        {
            _start = true;
            _rigidBody2D.WakeUp();
            _rigidBody2D.AddForceY(5, ForceMode2D.Impulse); 
            _instruccion.text = "";
            IniciarPalos(-2f);
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
        _buttonRef.action.Disable();
        PararPalos();
    }   

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("punto"))
        {
            if (collision.GetComponentInParent<SpriteRenderer>().color == Color.softRed)
            {
                _puntuacion += 5;
            }
            else
            {
                _puntuacion++;
            }
            _texto.text = "Puntos: " + _puntuacion;
            if (_puntuacion >= 80)
            {
                IniciarPalos(-7f);
            }else if (_puntuacion >= 40)
            {
                IniciarPalos(-5f);
            }else if (_puntuacion >= 20)
            {
                IniciarPalos(-4f);
            }else if (_puntuacion >= 10)
            {
                IniciarPalos(-3f);
            }
        }
    }

    private void IniciarPalos(float _vel)
    {
        Rigidbody2D[] cuerpos = FindObjectsByType<Rigidbody2D>();

        foreach (Rigidbody2D rb in cuerpos)
        {
            if (rb.tag.StartsWith("set"))
            {
                rb.linearVelocityX = _vel;
            }
        }
    }

    private void PararPalos()
    {
        Rigidbody2D[] cuerpos = FindObjectsByType<Rigidbody2D>();

        foreach (Rigidbody2D rb in cuerpos)
        {
            if (rb.tag.StartsWith("set"))
            {
                rb.linearVelocityX = 0;
            }
        }
    }
}
