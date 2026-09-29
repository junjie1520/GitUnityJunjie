using System;
using Unity.VisualScripting;
using UnityEngine;

public class CaleSpawner : MonoBehaviour
{
    [SerializeField]
    private CaleBehaviour _calePrefab;
    
     [SerializeField]
    private Transform _birle;

    [SerializeField]
    private PuntuacionManager _puntuacionManager;

    public void Spawn(int _puntuacion)
    {
        int _x = 15;
        if (_puntuacion >= 80)
        {
            _x = 2;
        }else if (_puntuacion >= 50)
        {
            _x = 5;
        }else if (_puntuacion >= 30)
        {
            _x = 10;
        }
        if (UnityEngine.Random.Range(1,_x) == 1)
        {
            float _y = UnityEngine.Random.Range(-3f, 3f);
            CaleBehaviour _nuevoCale = Instantiate(_calePrefab);
            _nuevoCale.SetBirle(_birle);
            _nuevoCale.transform.position = new Vector2(18, _y);
        }
    }

    public void Awake()
    {
        _puntuacionManager.OnSumarPuntos += Spawn;
    }
    public void Oestroy()
    {
        _puntuacionManager.OnSumarPuntos -= Spawn;
    }
}
