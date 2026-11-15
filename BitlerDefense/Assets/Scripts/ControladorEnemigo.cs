using System;
using UnityEngine;

public class ControladorEnemigo : MonoBehaviour
{
 
    private SOEnemigos _enemy;

    private int _hp;
    private float _speed;
    private int _damage;

    private bool _dead = false;

    public event Action<Vector3> OnMuerte;

    void Awake()
    {
        _Enemyrigidbody = GetComponent<Rigidbody2D>();
  
    }

    public void Inicializar(SOEnemigos SO)
    {
        _enemy = SO;
        _hp = _enemy.HP;
        _speed = _enemy.Speed;
        _damage =_enemy.Damage;
    }

    void Update()
    {
        if (_dead) return;

    }

    private void Death()
    {
        _dead = true;
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_dead) return;
    }
}
