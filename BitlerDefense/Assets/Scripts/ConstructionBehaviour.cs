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
    private int _level;
    public int Level
    {
        get{return _level;}
        set{_level = value;
            AddLevel();}
    }
    [SerializeField]
    private int _damage;

    [SerializeField]
    private GameObject _laserPrefab;
    private Coroutine _laserCoroutine;
    private GameObject _activeLaser;
    private List<GameObject> _enemyList = new List<GameObject>();

    public SOConstruction Construction => _contruction;

    public void Init(SOConstruction construction)
    {
        if (_level == 0)
        {
            _level = 1;
        }
        _contruction = construction;
        if (construction == null) return;
        AddLevel();
        GetComponent<CircleCollider2D>().enabled = construction.Type != SOConstruction.ConstructionType.Mine;
    }

    private void OnTriggerEnter2D(Collider2D co)
    {
        if (co.GetComponent<EnemyController>().Enemy.Type == SOEnemigos.EnemyType.Phantom)
        {
            if (_contruction.Type == SOConstruction.ConstructionType.Bomb)
            {
                _enemyList.Add(co.gameObject);
            }
        }
        else
        {
            _enemyList.Add(co.gameObject);
        }
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
        _enemyList.Remove(null);
        if (_enemyList.Count == 0)
        {
            return;
        }

        switch (_contruction.Type)
        {
            case SOConstruction.ConstructionType.Projectil:
                GameObject atack = Instantiate(_AtackPrefab, transform.position, Quaternion.identity);
                atack.GetComponent<AtackBehaviour>().Init(_enemyList[0], _contruction.soAtack, _damage);
                break;
            case SOConstruction.ConstructionType.Laser:
                if (_laserCoroutine == null)
                {
                    _laserCoroutine = StartCoroutine(LaserShoot(_enemyList[0]));
                }
                break;
            case SOConstruction.ConstructionType.Bomb:
                if (_contruction.soAtack != null)
                {
                    GameObject bomb = Instantiate(_AtackPrefab, transform.position, Quaternion.identity);
                    bomb.GetComponent<AtackBehaviour>().Init(null,_contruction.soAtack, 0);
                    foreach (GameObject candidate in _enemyList)
                    {
                        candidate.GetComponent<EnemyController>().Damaged(_contruction.Damage);
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

        while (enemy != null && _enemyList.Contains(enemy))
        {
            Vector2 posTurret = transform.position;
            Vector2 posEnemy = enemy.transform.position;
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
                        enemyScript.Damaged(_damage);
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

    private void AddLevel()
    {
        switch (_level)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = _contruction.Sprite1;
                _damage = _contruction.Damage;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = _contruction.Sprite2;
                _damage = _contruction.Damage *2;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = _contruction.Sprite3;
                _damage = _contruction.Damage *3;
                break; 
            default:
                break;
        }
    }
}