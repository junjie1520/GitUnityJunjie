using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    public EnemyController Spawn(SOEnemigos enemyType)
    {

        GameObject enemy = Instantiate(
                _enemyPrefab,
                transform.position,
                Quaternion.identity
            );
        EnemyController enemyC = enemy.GetComponent<EnemyController>();
        enemyC.Inicializar(enemyType);
        return enemyC;
    }
}
