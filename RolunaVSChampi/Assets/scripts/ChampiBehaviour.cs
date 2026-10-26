using System.Collections;
using UnityEngine;
public class ChampiBehaviour : MonoBehaviour
{
    private int _vida = 7;
    private float _velocidad = 1.5f;
    private float _fuerzaSalto = 5f;
    private Rigidbody2D _champi;
    private Rigidbody2D _roluna;
    private Animator _animator;
    private Coroutine _attack;
    private bool _attacking = false;
    private bool _canJump = true;
    private bool _touchingPlayer = false;
    private float _distanciaMin = 1.05f;
    private float _velAttacking = 1f;
    private float _difAltura = 1.5f;
    private float _zonaMuerta = 0.05f;
    
    void Awake()
    {
        _champi = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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

        if (_touchingPlayer)
        {
            _champi.linearVelocityX = 0f;
            if (_attack == null)
            {
                _attack = StartCoroutine(Attack());
            }
            return;
        }

        float difX = _roluna.position.x - _champi.position.x;
        float difY = _roluna.position.y - _champi.position.y;

        if (difX > _zonaMuerta)
        {
            _champi.linearVelocityX = _velocidad;
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            _animator.Play("Mushroom_Run");
        }
        else if (difX < -_zonaMuerta)
        {
            _champi.linearVelocityX = -_velocidad;
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            _animator.Play("Mushroom_Run");
        }
        else
        {
            _champi.linearVelocityX = 0f;
            _animator.Play("Mushroom_Idle");
        }

        if (_canJump && difY > _difAltura && difX < _distanciaMin && difX > -_distanciaMin)
        {
            _canJump = false;
            _champi.AddForceY(_fuerzaSalto, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (_champi.linearVelocity.y < 0)
        {
            _champi.gravityScale = 3f;
        }
        else
        {
            _champi.gravityScale = 1.0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider is BoxCollider2D)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _touchingPlayer = true;
            }
            else if (collision.gameObject.CompareTag("Suelo"))
            {
                _canJump = true;
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.otherCollider is BoxCollider2D)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _touchingPlayer = false;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _roluna == null)
        {
            _roluna = collision.GetComponent<Rigidbody2D>();
        }
    }


}
