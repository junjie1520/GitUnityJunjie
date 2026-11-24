using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] // Luego quitar esto, es para pruebas
    private SOEnemigos _enemy;
    public SOEnemigos Enemy => _enemy;
    [SerializeField] // Luego quitar esto, es para pruebas
    private int _hp;
    private string _direction;
    private string _nextDirection = "";
    private Vector2 _nodeCenter;
    public string Direction => _direction;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    public event Action<EnemyController> OnDeath;
    private bool _dead;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    public void Inicializar(SOEnemigos SO)
    {
        _enemy = SO;
        _hp = _enemy.HP;
 
        ChangeDirection(_enemy.Direction, transform.position);

        _animator.runtimeAnimatorController = _enemy.AnimatorController;

        _animator.Play("FullHP");
    }

    void FixedUpdate()
    {
        if (_nextDirection == "") return;

        Vector2 haciaNodo = _nodeCenter - _rigidbody.position;
        float _step = _enemy.Speed * Time.fixedDeltaTime;
        if (Vector2.Dot(haciaNodo, _rigidbody.linearVelocity.normalized) <= _step)
        {
            ChangeDirection(_nextDirection, _nodeCenter);
            _nextDirection = "";
        }
    }

    private void Death()
    {
        _rigidbody.linearVelocity = Vector2.zero;
        _nextDirection = "";
        _collider.enabled = false;
        Destroy(gameObject, 0.667f);
    }

    public void SetNextDirection(string direction, Vector3 nodeCenter)
    {
        _nextDirection = direction;
        _nodeCenter = nodeCenter;
    }

    public void ChangeDirection(string direction, Vector3 nodeCenter)
    {
        _rigidbody.position = nodeCenter;
        _direction = direction;
        if (_direction.Equals("UP"))
        {
            _rigidbody.linearVelocity = new Vector2(0f,_enemy.Speed);
        }else if (_direction.Equals("DOWN"))
        {
            _rigidbody.linearVelocity = new Vector2(0f,-_enemy.Speed);
        }else if (_direction.Equals("LEFT"))
        {
            _spriteRenderer.flipX = true;
            _rigidbody.linearVelocity = new Vector2(-_enemy.Speed,0f);
        }else if (_direction.Equals("RIGHT"))
        {
            _spriteRenderer.flipX = false;
            _rigidbody.linearVelocity = new Vector2(_enemy.Speed,0f);
        }
    }

    public void Damaged(int damage)
    {
        if (_dead) return;
        _hp -= damage;
        if (_hp <= 0)
        {
            _dead = true;
            OnDeath?.Invoke(this);
            _animator.Play("Dead");
            Death();
        }
        else if (_hp <= (_enemy.HP / 4f))
        {
            _animator.Play("FaseTerminal");
        }
        else if (_hp <= (_enemy.HP / 2f))
        {
            _animator.Play("Fase2");
        }
    }
}
