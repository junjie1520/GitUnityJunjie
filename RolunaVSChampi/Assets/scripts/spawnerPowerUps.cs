using UnityEngine;

public class spawnerPowerUps : MonoBehaviour
{
    private float _time;
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private SOPowerUp[] _powerUps;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if (_time >= 7)
        {
            Spawn();
            _time = 0f;
        }

    }
    private void Spawn()
    {
        GameObject powerup = Instantiate(_prefab);
        int c = Random.Range(-2, 54);
        int x = Random.Range(0, _powerUps.Length);
        SOPowerUp selectedPowerUp = _powerUps[x];
        powerup.transform.position = new Vector3(
            c,
            27,
            0
            );

        powerup.GetComponent<SpriteRenderer>().sprite =
            selectedPowerUp.Sprite;

       
        powerup.tag = selectedPowerUp.Tag;

    }
}

