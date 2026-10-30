
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private RolunaBehaviour _roluna;

    public void setVida(int vida)
    {
        _roluna.setVida(vida);
    }

    public int getVida()
    {
        return _roluna.getVida();
    }

    public void setAtaque(int ataque)
    {
        _roluna.setAtaque(ataque);
    }

    public int getAtaque()
    {
        return _roluna.getAtaque();
    }

    public int getPuntos()
    {
        return _roluna.getPuntos();
    }

    public void setPuntos(int puntos)
    {
        _roluna.setPuntos(puntos);
    }

    public void Invencible(int vida)
    {
        StartCoroutine(InvencibleCoroutine(vida));
    }

    private IEnumerator InvencibleCoroutine(int vida)
    {

        int vidaOriginal = getVida();

        Debug.Log("Invencibilidad activada");
        Debug.Log("Vida original: " + vidaOriginal);

        setVida(vida);

        Debug.Log("Vida invencible: " + getVida());

        yield return new WaitForSeconds(6f);

        setVida(vidaOriginal);

        Debug.Log("Invencibilidad terminada");
        Debug.Log("Vida restaurada: " + getVida());
    }

    public void Curacio(int cura)
    {
  

        Debug.Log("Curacion recogida");
        Debug.Log("Vida antes: " + getVida());

        setVida(Mathf.Min(getVida() + cura, 10));

        Debug.Log("Vida despues: " + getVida());
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
 