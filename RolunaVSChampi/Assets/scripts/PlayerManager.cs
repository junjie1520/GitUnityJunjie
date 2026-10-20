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

}
