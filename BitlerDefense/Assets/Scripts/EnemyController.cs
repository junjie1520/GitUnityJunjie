using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] // Luego quitar esto, es para pruebas
    private SOEnemigos _enemy;
    private int _hp;
    private string _direction;
    private string _nextDirection = "";
    private Vector2 _nodeCenter;
    public string Direction => _direction;
    private Rigidbody2D _rigidbody;
    public event Action<Vector3> OnMuerte;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Inicializar(_enemy);
    }

    public void Inicializar(SOEnemigos SO)
    {
        _enemy = SO;
        _hp = _enemy.HP;
        GetComponent<SpriteRenderer>().sprite = _enemy.Sprite;
        ChangeDirection(_enemy.Direction, transform.position);
    }

    void FixedUpdate()
    {
        if (_nextDirection  == "")
        {
            return;
        }

        float _step = _enemy.Speed * Time.fixedDeltaTime;
        if (Vector2.Distance(_rigidbody.position, _nodeCenter) <= _step)
            {
                ChangeDirection(_nextDirection, _nodeCenter);
                _nextDirection = "";
            }
        }

    private void Death()
    {
        Destroy(gameObject);
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
            _rigidbody.linearVelocity = new Vector2(-_enemy.Speed,0f);
        }else if (_direction.Equals("RIGHT"))
        {
            _rigidbody.linearVelocity = new Vector2(_enemy.Speed,0f);
        }
    }

    public void Damaged(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Death();
        }
    }
}
