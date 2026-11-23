using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using static SOEnemigos;

public class GamemanagerChris : MonoBehaviour
{
    [Serializable]
    public struct Wave
    {
        public float cooldown;
        public List<SOEnemigos> enemyswave;
    }
    private SpawnerEnemy _spawner;
    [SerializeField]
    private List<Wave> _waves;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _spawner = GetComponent<SpawnerEnemy>();
    }

    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn(){

        foreach (var round in _waves)
        {
            foreach (var enemy in round.enemyswave)
            {
                _spawner.Spawn(enemy);
                yield return new WaitForSeconds(round.cooldown);
            }
            yield return new WaitForSeconds(5);
        }

    }

}
