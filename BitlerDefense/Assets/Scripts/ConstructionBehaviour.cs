using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ConstructionBehaviour : MonoBehaviour
{


    [SerializeField]
    private SOConstruction _contruction;

    private Coroutine _shootCoroutine;

    [SerializeField]
    private GameObject _AtackPrefab;


    [SerializeField]
    private GameObject _laserPrefab;

    private List<GameObject> _enemyList = new List<GameObject>();

    private int _nivel = 1;

    void Init(SOConstruction construction)
    {
        _contruction = construction;
        GetComponent<SpriteRenderer>().sprite = construction.Sprite;
        if (_contruction.Type == SOConstruction.ConstructionType.Mine)
        {
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D co)
    {
        _enemyList.Add(co.gameObject);
        Debug.Log(_enemyList);

        if (_shootCoroutine == null)
        {
            _shootCoroutine = StartCoroutine(ShootCoroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D co)
    {
        _enemyList.Remove(co.gameObject);
    }

    void Awake()
    {
        Init(_contruction);
    }

    private IEnumerator ShootCoroutine()
    {
        Atack();
        yield return new WaitForSeconds(_contruction.Cooldown);
        _shootCoroutine = null;
        if (_enemyList.Count > 0)
        {
            _shootCoroutine = StartCoroutine(ShootCoroutine());
        }
    }

    private void Atack()
    {

        Debug.Log(_contruction.Type);

        switch (_contruction.Type)
        {
            case SOConstruction.ConstructionType.Projectil:
                GameObject atack = Instantiate(_AtackPrefab, transform.position, Quaternion.identity);
                atack.GetComponent<AtackBehaviour>().Init(_enemyList[0], _contruction.soAtack, _contruction.Type);
                break;
            case SOConstruction.ConstructionType.Laser:
                StartCoroutine(LaserShoot(_enemyList[0]));
                break;
            case SOConstruction.ConstructionType.Bomb:
                foreach (GameObject enemy in _enemyList)
                {
                    if (enemy.TryGetComponent<EnemyController>(out EnemyController enemyScript))
                    {
                        enemyScript.Damaged(_contruction.soAtack.Damage);
                    }
                }
                break;


        }
    }

    private IEnumerator LaserShoot(GameObject enemy)
    {
        GameObject laser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
        while (enemy != null)
        {
            Vector2 posTurret = transform.position;
            Vector2 posEnemy = enemy.transform.position;
            Vector2 direction = (posEnemy - posTurret).normalized;
            float distance = direction.magnitude;
            laser.transform.right = direction;
            laser.transform.localScale = new Vector3 (distance,1,1);

            RaycastHit2D[] list = Physics2D.RaycastAll(posTurret, direction, distance, 7);
            foreach (RaycastHit2D enemyHitted in list)
            {
                if (enemy.TryGetComponent<EnemyController>(out EnemyController enemyScript))
                {
                    enemyScript.Damaged(_contruction.soAtack.Damage);
                }   
            }

            yield return new WaitForSeconds(_contruction.soAtack.Speed);
        }

        Destroy(laser);

    }
}