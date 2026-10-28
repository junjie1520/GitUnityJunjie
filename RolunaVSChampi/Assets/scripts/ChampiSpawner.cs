using System;
using UnityEngine;

public class ChampiSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] _spawnPoints;
    [SerializeField]
    private ChampiBehaviour _champi;
    
    public void Spawn(int cantidad)
    {
        for (int i = 0; i != cantidad; i++)
        {
            Transform t = _spawnPoints[UnityEngine.Random.Range(0,11)];
            Instantiate(_champi, new Vector3(t.position.x,t.position.y), Quaternion.identity);
        }
    }
}
