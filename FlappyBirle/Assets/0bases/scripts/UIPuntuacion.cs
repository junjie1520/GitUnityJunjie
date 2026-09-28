using System;
using TMPro;
using UnityEngine;

public class UIPuntuacion : MonoBehaviour
{
    private TextMeshProUGUI _text;

    [SerializeField]
    private PuntuacionManager _puntuacionManager;

    void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _puntuacionManager.OnSumarPuntos += ActualitzarUI;
    }

    void OnDestroy()
    {
        _puntuacionManager.OnSumarPuntos -= ActualitzarUI;
    }

    private void ActualitzarUI(int _puntos)
        {
            _text.text = "Puntos: " + _puntos;
        }
}
