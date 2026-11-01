
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private RolunaBehaviour _roluna;


    public void AplicarPowerUp(SOPowerUp powerUp)
    {
        _roluna.Vida += powerUp.Vida;
        _roluna.Velocidad += powerUp.Velocidad;
        _roluna.Salto += powerUp.Salto;
        _roluna.Cooldown -= powerUp.Cooldown;

        if (powerUp.Corutina){
            StartCoroutine(CorutinaPowerUp(powerUp));
        }
    }

    private IEnumerator CorutinaPowerUp(SOPowerUp powerUp)
    {
        yield return new WaitForSeconds(powerUp.Duracion);

        _roluna.Vida -= powerUp.Vida;
        _roluna.Velocidad -= powerUp.Velocidad;
        _roluna.Salto -= powerUp.Salto;
        _roluna.Cooldown += powerUp.Cooldown;
    }
}
 