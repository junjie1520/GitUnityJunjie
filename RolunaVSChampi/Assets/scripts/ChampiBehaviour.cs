using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class ChampiBehaviour : MonoBehaviour
{
    private int _vida = 7;
    private float _velocidad = 4f;
    private float _fuerzaSalto = 7f; // ~2.5 unidades de altura: supera muros de 2 tiles
    private Rigidbody2D _champi;
    private Transform _roluna;
    private Animator _animator;
    private Coroutine _attack;
    private bool _attacking = false;
    private bool _canJump = true;
    private bool _touchingPlayer = false;
    private bool _touching = false;
    private float _distanciaMin = 1.05f;
    private float _velAttacking = 1f;
    private float _difAltura = 1.5f;
    private float _zonaMuerta = 0.05f;
    private float _tempoStuck = 0f;
    private float _lastPos;
    
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
        if (!_touching)
        {
            Debug.Log(name + " canJump: " + _canJump + " | stuck: " + _tempoStuck + " | velY: " + _champi.linearVelocity.y);
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
                _tempoStuck = 0f;
            }

            float movidoX = _champi.position.x - _lastPos;
            if (movidoX < 0.01f && movidoX > -0.01f && _champi.linearVelocityX != 0f)
            {
                _tempoStuck += Time.deltaTime;
            }
            else if (movidoX >= 0.01f || movidoX <= -0.01f)
            {
                _tempoStuck = 0f;
                _lastPos = _champi.position.x;
            }

            if (_canJump && _tempoStuck >= 2f)
            {
                _touching = true;
                _canJump = false;
                _champi.linearVelocityX = 0f;
                _champi.AddForceY(_fuerzaSalto, ForceMode2D.Impulse);
                _tempoStuck = 0f;
            }
        }
        else
        {
            _champi.linearVelocityX = 0f;
            _animator.Play("Mushroom_Idle");
            if (_champi.linearVelocity.y < 0f)
            {
                _touching = false;
            }
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
            if (collision.gameObject.layer == 3)
            {
                _touchingPlayer = true;
            }
            if (collision.gameObject.layer == 7)
            {
                _vida -= collision.gameObject.GetComponent<BalaBehaviour>().dany;
            }
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.otherCollider is BoxCollider2D && (collision.gameObject.CompareTag("Suelo") || collision.gameObject.layer == 6))
        {
            if (_champi.linearVelocity.y <= 0.01f)
            {
                for (int i = 0; i < collision.contactCount; i++)
                {
                    Debug.Log(name + " STAY -> normalY: " + collision.GetContact(i).normal.y + " | puntoY: " + collision.GetContact(i).point.y + " | piesY: " + _champi.position.y);
                    // Contacto a la altura de los pies (el pivot de Champi está abajo)
                    if (collision.GetContact(i).point.y < _champi.position.y + 0.05f)
                    {
                        _canJump = true;
                    }
                }
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.otherCollider is BoxCollider2D)
        {
            if (collision.gameObject.layer == 3)
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
        if (collision.gameObject.layer == 3 && _roluna == null)
        {
            _roluna = collision.GetComponent<Transform>();
        }
    }


}
