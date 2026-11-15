using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ConstructionBehaviour : MonoBehaviour
{

    private SOProjectile _soProjectil;

    [SerializeField]
    private SOConstruction _contruction;

    private Coroutine _shootCoroutine;

    private GameObject _projectilePrefab; 

    private List<GameObject> _enemyList = new List<GameObject>();    

    void Init(SOConstruction construction){
        _contruction = construction;
        GetComponent<SpriteRenderer>().sprite = construction.Sprite;
        if(_contruction.Type == SOConstruction.ConstructionType.Mine){
            _contruction.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D co){
        _enemyList.Add(co.gameObject);
        if (_shootCoroutine != null){
            _shootCoroutine = StartCoroutine(ShootCoroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D co){
        _enemyList.Remove(co.gameObject);
    }

    void Awake(){
        Init(_contruction);
    }

    

    private IEnumerator ShootCoroutine()
    {

        Atack();
        yield return new WaitForSeconds(_contruction.Cooldown);
        _shootCoroutine = null;
        if(_enemyList.Count > 0){
            _shootCoroutine = StartCoroutine(ShootCoroutine());
        }
    }

    private void Atack(){

        GameObject enemy = _enemyList[0];
        switch (_contruction.Type)
        {
            case SOConstruction.ConstructionType.Projectil:
                Debug.Log("proyectil");
                GameObject projectil = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
                projectil.GetComponent<ProjectileBehaviour>().Init(enemy, _soProjectil, _contruction.Type);
                break;
            case SOConstruction.ConstructionType.Laser:
                Debug.Log("Laser");
                break;
            case SOConstruction.ConstructionType.Bomb:
                Debug.Log("bomb");
                break;

        }
    }

}