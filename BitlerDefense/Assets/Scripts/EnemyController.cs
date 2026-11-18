using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] // Luego quitar esto, es para pruebas
    private SOEnemigos _enemy;
    [SerializeField]
    private int _hp;
    private string _direction;
    private string _nextDirection = "";
    private Vector2 _nodeCenter;
    private int _id;
    private float _timer;
    public string Direction => _direction;
    private Rigidbody2D _rigidbody;
    public event Action<Vector3> OnMuerte;

    private Animator _animator;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }


    public void Inicializar(SOEnemigos SO, int id)
    {

        _id = id;
        _enemy = SO;
        _hp = _enemy.HP;
 
        ChangeDirection(_enemy.Direction, transform.position);

        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = _enemy.AnimatorController;

        _animator.Play("FullHP");
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

    private void Update()
    {
        /*

        Timer desactivado porque estaba haciendo pruebas en la escena de la nuria

        _timer+= Time.deltaTime;
        if (_timer >= 1)
        {
            Damaged(1);
            _timer -= 1;
        }
        */
    }

    private void Death()
    {
        Destroy(gameObject,0.667f);
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
        // Debug si atacan, para no mirar el SerializeField todo el rato
        Debug.Log("Me atacan: "+_hp);
        if (_hp <= 0)
        {
          
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
