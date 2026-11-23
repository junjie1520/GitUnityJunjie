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
    private Coroutine _laserCoroutine;
    private GameObject _activeLaser;
    private List<GameObject> _enemyList = new List<GameObject>();

    public SOConstruction Construction => _contruction;

    public void Init(SOConstruction construction)
    {
        _contruction = construction;
        if (construction == null) return;
        GetComponent<SpriteRenderer>().sprite = construction.Sprite;
        GetComponent<CircleCollider2D>().enabled = construction.Type != SOConstruction.ConstructionType.Mine;
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
        if (_contruction != null) Init(_contruction);
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
        // si el enemigo muere, quitarlo de la lista de enemigos
        for (int i = _enemyList.Count - 1; i >= 0; i--)
        {
            if (_enemyList[i] == null)
            {
                _enemyList.RemoveAt(i);
            }
        }
        // Si no hay enemigos, no ataca
        if (_enemyList.Count == 0)
        {
            return;
        }
        // ---------------------------------------

        switch (_contruction.Type)
        {
            case SOConstruction.ConstructionType.Projectil:
                GameObject atack = Instantiate(_AtackPrefab, transform.position, Quaternion.identity);
                atack.GetComponent<AtackBehaviour>().Init(_enemyList[0], _contruction.soAtack, _contruction.Type);
                break;
            case SOConstruction.ConstructionType.Laser:
            // Si ya esta lanzando un laser, que no lance otro, muy bien Nuria creado un laser cada tick :)
                if (_laserCoroutine == null)
                {
                    _laserCoroutine = StartCoroutine(LaserShoot(_enemyList[0]));
                }
                break;
            case SOConstruction.ConstructionType.Bomb:
                if (_contruction.soAtack != null)
                {
                    foreach (GameObject candidate in _enemyList)
                    {
                        if (!candidate.TryGetComponent<EnemyController>(out _)) continue;
                        GameObject bomb = new GameObject("BombEffect");
                        bomb.transform.position = transform.position;
                        bomb.AddComponent<BombAttackEffect>().Init(candidate, _contruction.soAtack, GetComponent<SpriteRenderer>());
                        break;
                    }
                }
                break;


        }
    }

    private IEnumerator LaserShoot(GameObject enemy)
    {
        GameObject laser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
        _activeLaser = laser;
        Animator laserAnimator = laser.GetComponent<Animator>();
        if (laserAnimator != null && _contruction.soAtack.Animation != null)
            laserAnimator.runtimeAnimatorController = _contruction.soAtack.Animation;
        float _damageTime = 0f;

        // si el enemigo ha salido o muerto, parar de acatar
        while (enemy != null && _enemyList.Contains(enemy))
        {
            Vector2 posTurret = transform.position;
            Vector2 posEnemy = enemy.transform.position;
            // la diferencia y distancia calculada por separado, para luego usarla para la scale X del laser
            Vector2 diferencia = posEnemy - posTurret;
            Vector2 direction = diferencia.normalized;
            float distance = diferencia.magnitude;

            laser.transform.right = direction;
            laser.transform.localScale = new Vector3 (distance,1,1);

            _damageTime -= Time.deltaTime;
            if (_damageTime <= 0f)
            {
                _damageTime = _contruction.Cooldown;

                RaycastHit2D[] list = Physics2D.RaycastAll(posTurret, direction, distance, LayerMask.GetMask("Enemy"));
                foreach (RaycastHit2D enemyHitted in list)
                {
                    if (enemyHitted.collider.gameObject.TryGetComponent<EnemyController>(out EnemyController enemyScript))
                    {
                        enemyScript.Damaged(_contruction.soAtack.Damage);
                    }
                }
            }
            yield return null;
        }

        Destroy(laser);
        _activeLaser = null;
        _laserCoroutine = null;
    }

    private void OnDestroy()
    {
        if (_activeLaser != null) Destroy(_activeLaser);
    }
}