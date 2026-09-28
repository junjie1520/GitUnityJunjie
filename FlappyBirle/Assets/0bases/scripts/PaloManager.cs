using UnityEngine;

public class PaloManager : MonoBehaviour
{
    private Rigidbody2D _palo;

    void Awake()
    {
        _palo = GetComponent<Rigidbody2D>();
    }

    public void IniciarPalo(float _vel)
    {
        _palo.linearVelocityX = _vel;
    }

    public void PararPalo()
    {
        _palo.linearVelocityX = 0;
    }
}