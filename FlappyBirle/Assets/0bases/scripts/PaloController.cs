using UnityEngine;

public class PalosController : MonoBehaviour
{
    [SerializeField]
    private PaloManager[] _palos;

    public void IniciarPalos(float _vel)
    {
        foreach (PaloManager _palo in _palos)
        {
            _palo.IniciarPalo(_vel);
        }
    }

    public void PararPalos()
    {
        foreach (PaloManager _palo in _palos)
        {
            _palo.PararPalo();
        }
    }
}