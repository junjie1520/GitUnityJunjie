using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Serializable]
    public struct Wave
    {
        public float _cooldown;
        public List<SOEnemigos> _wave;
    }
    private SpawnerEnemy _spawner;
    [SerializeField]
    private List<Wave> _waves;
    public event Action<bool> OnLevelComplete;
    private int _alive;
    private bool _spawnFinish;
    private int _countdown = 10;
    void Awake()
    {
        _spawner = GetComponent<SpawnerEnemy>();
    }

    public void StartLevel()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (_countdown != 0)
        {
            yield return new WaitForSeconds(1f);
            _countdown--;
        }
        foreach (var round in _waves)
        {
            foreach (var enemy in round._wave)
            {
                EnemyController e = _spawner.Spawn(enemy);
                _alive++;
                e.OnDeath += EnemyDeath;
                yield return new WaitForSeconds(round._cooldown);
            }
            while (_alive != 0)
            {
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(5f);
        }
        _spawnFinish = true;
        CheckEnd();
    }

    private void EnemyDeath(EnemyController e)
    {
        e.OnDeath -= EnemyDeath;
        _alive--;
        CheckEnd();
    }

    private void CheckEnd()
    {
        if (_spawnFinish && _alive == 0)
        {
            OnLevelComplete?.Invoke(true);
        }
    }
}
