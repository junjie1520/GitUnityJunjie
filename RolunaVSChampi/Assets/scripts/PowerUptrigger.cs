
using UnityEngine;

public class PowerUptrigger : MonoBehaviour
{
    private SOPowerUp _powerUp;
    private PlayerManager _playerManager;

    public void SetPowerUp(SOPowerUp powerUp, PlayerManager playerManager)
    {
        _powerUp = powerUp;
        _playerManager = playerManager;

        GetComponent<SpriteRenderer>().sprite = _powerUp.Sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            _playerManager.AplicarPowerUp(_powerUp);
            Destroy(gameObject);
        }
    }
}
