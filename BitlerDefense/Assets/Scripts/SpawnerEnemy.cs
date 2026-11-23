
using UnityEngine;
using static SOEnemigos;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    private int _id = 0;

    public void Spawn(SOEnemigos enemyType)
    {

        GameObject evil = Instantiate(
                _enemyPrefab,
                transform.position,
                Quaternion.identity
            );


        evil.GetComponent<EnemyController>().Inicializar(enemyType,_id++);

    }
}
