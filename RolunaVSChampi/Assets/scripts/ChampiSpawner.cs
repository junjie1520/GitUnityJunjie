using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ChampiSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] _spawnPoints;
    [SerializeField]
    private ChampiBehaviour _champi;
    public event Action<Vector3> OnChampiMuerto;

    public void Spawn(int cantidad)
    {
        for (int i = 0; i != cantidad; i++)
        {
            Transform t = _spawnPoints[UnityEngine.Random.Range(0,11)];
            ChampiBehaviour _nuevoChampi = Instantiate(_champi, new Vector3(t.position.x, t.position.y), Quaternion.identity);
            _nuevoChampi.OnMuerte += ChampiMuerto;
        }
    }
    private void ChampiMuerto(Vector3 posicion)
    {
        OnChampiMuerto?.Invoke(posicion);
    }
}
