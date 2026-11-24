using System;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    public event Action<bool> OnBaseDestroyed;
    private int _hp = 10;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_hp <= 0) return;
        _hp--;
        if (_hp <= 0) OnBaseDestroyed?.Invoke(false);
        collision.gameObject.GetComponent<EnemyController>().Damaged(999);
    }
}
