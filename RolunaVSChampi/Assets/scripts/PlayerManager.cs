
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private RolunaBehaviour _roluna;


    public void Invencible(int vida)
    {
        StartCoroutine(InvencibleCoroutine(vida));
    }

    private IEnumerator InvencibleCoroutine(int vida)
    {

        int vidaOriginal = _roluna.Vida;

        Debug.Log("Invencibilidad activada");
        Debug.Log("Vida original: " + vidaOriginal);

        _roluna.Vida = vida;

        Debug.Log("Vida invencible: " + _roluna.Vida);

        yield return new WaitForSeconds(6f);

        _roluna.Vida = vidaOriginal;

        Debug.Log("Invencibilidad terminada");
        Debug.Log("Vida restaurada: " + _roluna.Vida);
    }

    public void Curacio(int cura)
    {
  

        Debug.Log("Curacion recogida");
        Debug.Log("Vida antes: " + _roluna.Vida);

        _roluna.Vida = (Mathf.Min(_roluna.Vida + cura, 10));

        Debug.Log("Vida despues: " + _roluna.Vida);
    }

    /*public void Nitro(float velocidad, float salto)
    {
        StartCoroutine(NitroCoroutine(velocidad, salto));
    }

    private IEnumerator NitroCoroutine(float velocidad, float salto)
    {
        float velocidadOriginal = _roluna.getVelocidad();
        float saltoOriginal = _roluna.getSalto();

        nitroActivo = true;

        _roluna.setVelocidad(velocidad);
        _roluna.setSalto(salto);

        yield return new WaitForSeconds(10f);

        _roluna.setVelocidad(velocidadOriginal);
        _roluna.setSalto(saltoOriginal);

        nitroActivo = false;
    }

    */

    public void Berserker(int vida)
    {
        Debug.Log("Berserker recogido");
    }
}
 