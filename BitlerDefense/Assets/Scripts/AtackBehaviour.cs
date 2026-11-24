using System.Collections;
using UnityEngine;

public class AtackBehaviour : MonoBehaviour
{
    private GameObject _enemy;
    private float _speed;
    private int _damage;
    private Rigidbody2D _rigidBody;
    private bool _destroying = false;
    private Sprite _sprite;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Init(GameObject enemy, SOAtack SOAtack, int damage, bool isExplode)
    {
        _enemy = enemy;
        _speed = SOAtack.Speed;
        _damage = damage;
        _sprite = SOAtack.Sprite;
        GetComponent<SpriteRenderer>().sprite = _sprite;
        Animator animator = GetComponent<Animator>();
        if (animator != null && SOAtack.Animation != null) animator.runtimeAnimatorController = SOAtack.Animation;
        if (isExplode)
        {
            animator.Play("ExplosionLata");
            Destroy(gameObject,0.6f);
        }
    }

    void FixedUpdate()
    {
        if (_enemy == null)
        {
            if (!_destroying)
            {
                Destroy(gameObject,0.6f);
            }
            _destroying = true;
            return;
        }
        Vector2 direction = (_enemy.transform.position - transform.position).normalized;
        _rigidBody.linearVelocity = direction * _speed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyController>(out EnemyController enemyScript))
        {
            enemyScript.Damaged(_damage);
            Destroy(gameObject);
        }
    }
}