using NUnit.Framework;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using static SOEnemigos;

public class GamemanagerChris : MonoBehaviour
{
    [SerializeField]
    SpawnerEnemy _spawner;
    [SerializeField]
    private List<SOEnemigos> _enemigosOleada1;
    private List<List<SOEnemigos>> _waves = new List<List<SOEnemigos>>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        
    }
    void Start()
    {
        _waves.Add(_enemigosOleada1);
        _spawner.Spawn(_waves[0][0]);
    }

    
}
