using System.Collections;
using UnityEngine;

public class AtackBehaviour : MonoBehaviour
{
    private GameObject _enemy;
    private float _speed;
    private int _damage;
    private SOConstruction.ConstructionType _type;
    private Rigidbody2D _rigidBody;
    
    private Sprite _sprite;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Init(GameObject enemy, SOAtack SOAtack, SOConstruction.ConstructionType type)
    {
        _enemy = enemy;
        _speed = SOAtack.Speed;
        _damage = SOAtack.Damage;
        _type = type;
        _sprite = SOAtack.Sprite;
    }



    void FixedUpdate()
    {
        if (_enemy == null)
        {
            Destroy(gameObject);
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