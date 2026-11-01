using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private ChampiSpawner _spawner;
    private int _time = 150;
    private int _timeRemaining = 0;
    private int _spawnTime = 5;
    private int _spawnRemaining = 0;
    private int _cantidadChampi = 2;
    private int _puntos = 0;
    public event Action<int> OnSumarPuntos;
    public event Action<int> OnPasaSegundo;
    private static GameManager _instance;
    public static GameManager Singleton => _instance;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(_instance.gameObject);
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        _spawner.OnChampiMuerto += ChampiMuerto;
        _timeRemaining = _time;
        OnSumarPuntos?.Invoke(_puntos);
        OnPasaSegundo?.Invoke(_timeRemaining);
        Invoke("UnSegundo", 1f);
    }

    void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
        if (_spawner != null)
        {
            _spawner.OnChampiMuerto -= ChampiMuerto;
        }
    }

    private void UnSegundo()
    {
        _timeRemaining--;
        OnPasaSegundo?.Invoke(_timeRemaining);
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
            }else if (_timeRemaining <= 50)
            {
                _spawnTime = 3;
                _cantidadChampi = 3;
            }
        }
        else
        {
            EndGame();
        }
    }
    private void ChampiMuerto(Vector3 posicion)
    {
        SumarPunto();
    }
    private void SumarPunto()
    {
        _puntos++;
        OnSumarPuntos?.Invoke(_puntos);
    }

    public int Puntos
    {
        get { return _puntos; }
    }

    public int TiempoRestante
    {
        get { return _timeRemaining; }
    }

    public void EndGame()
    {
        CancelInvoke();
        SceneManager.LoadScene("End");
    }
}
