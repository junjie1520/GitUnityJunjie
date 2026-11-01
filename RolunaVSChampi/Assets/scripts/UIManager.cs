using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private RolunaBehaviour _roluna;

    [SerializeField]
    private TextMeshProUGUI _textoPuntos;
    [SerializeField]
    private TextMeshProUGUI _textoTiempo;
    [SerializeField]
    private TextMeshProUGUI _textoVida;

    void OnEnable()
    {
        _gameManager.OnSumarPuntos += ActualizarPuntos;
        _gameManager.OnPasaSegundo += ActualizarTiempo;
        _roluna.OnCambioVida += ActualizarVida;
    }

    void OnDisable()
    {
        _gameManager.OnSumarPuntos -= ActualizarPuntos;
        _gameManager.OnPasaSegundo -= ActualizarTiempo;
        _roluna.OnCambioVida -= ActualizarVida;
    }

    private void ActualizarPuntos(int puntos)
    {
        _textoPuntos.text = "Puntuacion: " + puntos;
    }

    private void ActualizarTiempo(int segundos)
    {
        int _minutos =segundos/60;
        int _segundos = segundos%60;
        string _texto = _minutos + ":";
        if (_segundos < 10)
        {
            _texto += "0";

        }
        _texto += _segundos;
        _textoTiempo.text = _texto;
    }

    private void ActualizarVida(int vida)
    {
        _textoVida.text = "x" + vida;
    }
}