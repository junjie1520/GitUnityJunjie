using UnityEngine;

public class PowerUpsSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private SOPowerUp[] _powerUps;
    private float _time = 0f;

    void Update()
    {
        _time += Time.deltaTime;
        if (_time >= 5)
        {
            _time -= 5f;
            Spawn();
        }
    }

    void Spawn()
    {
        
        int x = Random.Range(0, _powerUps.Length);
        int c = Random.Range(-12, 61);
        GameObject powerup = Instantiate(_prefab);

        powerup.tag = _powerUps[x].Tag;
        powerup.GetComponent<SpriteRenderer>().sprite = _powerUps[x].Sprite;
        powerup.transform.position = new Vector3(
            c,
            27,
            0);

    }
}
