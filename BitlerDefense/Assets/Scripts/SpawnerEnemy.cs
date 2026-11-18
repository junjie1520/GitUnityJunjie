
using UnityEngine;
using static SOEnemigos;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private SOEnemigos _type;

    private int _id = 0;

    private void Awake()
    {
        // Unos cuantos spawn para hacer pruebas, luego la quitas si quieres
        Spawn(_type);
        Spawn(_type);
        Spawn(_type);
        Spawn(_type);
    }
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
