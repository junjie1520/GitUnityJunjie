using UnityEngine;

public class SpawnerPowerUps : MonoBehaviour
{
    private float _time;
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private SOPowerUp[] _powerUps;
    [SerializeField]
    private ChampiSpawner _spawner;

    void Start()
    {
        _spawner.OnChampiMuerto += Spawn;
    }
    void OnDestroy()
    {
        if (_spawner != null)
        {
            _spawner.OnChampiMuerto -= Spawn;
        }
    }
    private void Spawn(Vector3 posicion)
    {
        if (Random.Range(0,4) == 0)
        {
            Vector3 _posicionPowerUp = new Vector3(posicion.x, posicion.y + 0.5f, 0f);
            GameObject powerup = Instantiate(_prefab, _posicionPowerUp, Quaternion.identity);
            int x = Random.Range(0, _powerUps.Length);
            powerup.GetComponent<PowerUptrigger>().SetPowerUp(_powerUps[x]);
        }
    }
}

