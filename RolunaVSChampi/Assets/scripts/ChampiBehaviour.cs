using System.Collections;
using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
public class ChampiBehaviour : MonoBehaviour
{
    private int _vida = 7;
    private float _velocidad = 1.5f;
    private Rigidbody2D _champi;
    private Rigidbody2D _roluna;
    private Animator _animator;
    private Coroutine _attack;
    private bool _attacking = false;
    private float _distanciaMin = 1.04f;
    private float _velAttacking = 1f;
    void Awake()
    {
        _champi = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _roluna == null)
        {
            _roluna = collision.GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        if (_vida < 1)
        {
            _animator.Play("Mushroom_Die");
            Destroy(gameObject,1.5f);
            return;
        }
        if (_roluna == null)
        {
            _animator.Play("Mushroom_Idle");
            _champi.linearVelocityX = 0f;
            return;
        }
        if (_attacking)
        {
            return;
        }
        if (_champi.position.x < _roluna.position.x - _distanciaMin)
        {
            _champi.linearVelocityX = _velocidad;
            _animator.Play("Mushroom_Run");
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if (_champi.position.x > _roluna.position.x + _distanciaMin)
        {
            _champi.linearVelocityX = -_velocidad;
            _animator.Play("Mushroom_Run");
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else
        {
            _champi.linearVelocityX = 0f;
            _animator.Play("Mushroom_Idle");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider is BoxCollider2D)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (_attack == null)
                {
                    _attack = StartCoroutine(Attack());
                }
            }
        }
        
    }
    private IEnumerator Attack()
    {
        _attacking = true;
        _animator.Play("Mushroom_Attack");

        if (_roluna != null)
        {
            if (_champi.position.x < _roluna.position.x)
            {
                _champi.linearVelocityX = _velAttacking;
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            else
            {
                _champi.linearVelocityX = -_velAttacking;
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
        }

        yield return new WaitForSeconds(1f);

        _champi.linearVelocityX = 0f;
        _attacking = false;
        _attack = null;
    }

}
