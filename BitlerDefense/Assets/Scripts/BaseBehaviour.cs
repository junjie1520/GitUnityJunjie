using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    private int _hp = 10;
    void OnCollisionEnter2D(Collision2D collision)
    {
        _hp--;
        EnemyController e = collision.gameObject.GetComponent<EnemyController>();
        e.Damaged(999);
        Debug.Log("HP: "+_hp);
    }
}
