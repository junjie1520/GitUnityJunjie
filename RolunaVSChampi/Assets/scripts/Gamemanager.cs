using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private ChampiSpawner _spawner;
    private int _time = 90;
    private int _timeRemaining;
    private int _spawnTime = 5;
    private int _spawnRemaining = 0;
    private int _cantidadChampi = 2;

    void Start()
    {
        Invoke("UnSegundo",1f);
        _timeRemaining = _time;
    }

    private void UnSegundo()
    {
        _timeRemaining--;
        _spawnRemaining--;
        if (_spawnRemaining < 0)
        {
            _spawnRemaining = _spawnTime;
            _spawner.Spawn(_cantidadChampi);
        }
        if (_timeRemaining > 0)
        {
            Invoke("UnSegundo",1f);
            if (_timeRemaining <= 20)
            {
                _spawnTime = 2;
                _cantidadChampi = 5;
                _spawnRemaining = 0;
            }else if (_timeRemaining <= 50)
            {
                _spawnTime = 3;
                _cantidadChampi = 3;
                _spawnRemaining = 0;
            }
        }
        else
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        
    }
}
