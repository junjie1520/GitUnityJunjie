
using UnityEngine;

public class PowerUptrigger : MonoBehaviour
{
    private SOPowerUp _powerUp;

    public void SetPowerUp(SOPowerUp powerUp)
    {
        _powerUp = powerUp;
        GetComponent<SpriteRenderer>().sprite = _powerUp.Sprite;

        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
            collider = gameObject.AddComponent<BoxCollider2D>();

        collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int vido = 200;
        int cura = 3;
        float velocidad = 9f;
        float salto = 15f;

        if (collision.GetComponentInParent<RolunaBehaviour>() != null)
        {
            PlayerManager playerManager = FindAnyObjectByType<PlayerManager>();

      

            Debug.Log("PowerUp recogido: " + _powerUp.Type);

            switch (_powerUp.Type)
            {
                case SOPowerUp.PowerUpType.Invencible:
                    playerManager.Invencible(vido);
                    break;

                case SOPowerUp.PowerUpType.Nitro:
                    //playerManager.Nitro(velocidad, salto);
                    break;

                case SOPowerUp.PowerUpType.Curacio:
                    playerManager.Curacio(cura);
                    break;

                case SOPowerUp.PowerUpType.Berserker:
                    playerManager.Berserker(vido);
                    break;
            }

            Destroy(gameObject);
        }
    }
}
