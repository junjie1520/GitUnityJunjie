using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private GameObject _enemy;
    private float _speed;
    private float _damage;
    private SOConstruction.ConstructionType _type;
    private Rigidbody2D _rigidBody;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Init(GameObject enemy, SOProjectile soProjectile, SOConstruction.ConstructionType type)
    {
        _enemy = enemy;
        _speed = soProjectile.Speed;
        _damage = soProjectile.Damage;
        _type = type;
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
        if (collision.CompareTag("Enemy"))
        {
            EnemyBehaviour enemyScript = collision.GetComponent<EnemyBehaviour>();
            if (enemyScript != null)
            {
                enemyScript.RecibirDaño(_damage);
            }

            switch (_type)
            {
                case SOConstruction.ConstructionType.Projectil:
                    Destroy(gameObject);
                    break;

                case SOConstruction.ConstructionType.Laser:
                    Destroy(gameObject);
                    break;
            }
        }
    }
}