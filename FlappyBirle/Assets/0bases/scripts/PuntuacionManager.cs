using TMPro;
using UnityEngine;
using System;

public class PuntuacionManager : MonoBehaviour
{
    private int _puntuacion;

    [SerializeField]
    private BirleBehaviour _birle;
    [SerializeField]
    private PalosController _paloManager;
    public event Action<int> OnSumarPuntos;
    void Awake()
    {
        _puntuacion = 0;
        _birle.OnBirleCollision += SumarPunto;
    }

    void OnDestroy()
    {
        _birle.OnBirleCollision -= SumarPunto;
    }

    public void SumarPunto(int _puntos)
    {
        _puntuacion += _puntos;
        OnSumarPuntos?.Invoke(_puntuacion);
        VelocidadPalos();
    }

    public void VelocidadPalos(){
        float _vel = -2f;
        if (_puntuacion >= 80)
        {
            _vel = -9f;
        }
        else if (_puntuacion >= 50)
        {
            _vel = -7f;
        }else if (_puntuacion >= 30)
        {
            _vel = -5f;
        }else if (_puntuacion >= 10)
        {
            _vel = -4f;
        }else if (_puntuacion >= 5)
        {
            _vel = -3f;
        }
        _paloManager.IniciarPalos(_vel);
    }
}
