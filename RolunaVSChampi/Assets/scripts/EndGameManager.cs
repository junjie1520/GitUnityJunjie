using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _titulo;
    [SerializeField]
    private TextMeshProUGUI _rango;
    [SerializeField]
    private TextMeshProUGUI _comentario;

    void Start()
    {
        GameManager _gm = GameManager.Singleton;
        if (_gm == null)
        {
            _titulo.text = "Sin partida";
            return;
        }
        if (_gm.TiempoRestante <= 0)
        {
            _titulo.text = "HAS SOBREVIVIDO!";
            if (_gm.Puntos <= 5)
            {
                _rango.text = "Tu rango: C";
                _comentario.text = "Lo de correr lo sabemos todos eh...";
            }else if (_gm.Puntos <= 10)
            {
                _rango.text = "Tu rango: B";
                _comentario.text = "Los Champis deben morir, esfuerzate";
            }else if (_gm.Puntos <= 25)
            {
                _rango.text = "Tu rango: A";
                _comentario.text = "GG";
            }else if (_gm.Puntos <= 50)
            {
                _rango.text = "Tu rango: A+";
                _comentario.text = "Well Played";
            }else if (_gm.Puntos == 67)
            {
                _rango.text = "Tu rango: 67";
                _comentario.text = "67!!!";
            }else if (_gm.Puntos <= 75)
            {
                _rango.text = "Tu rango: S";
                _comentario.text = "EZ";
            }else if (_gm.Puntos > 75)
            {
                _rango.text = "Tu rango: S+";
                _comentario.text = "Porque malgastas tiempo en tu vida jugando a esto???";
            }
        }
        else
        {
            _titulo.color = Color.red;
            _titulo.text = "HAS MUERTO!";
            _rango.color = Color.red;
            _rango.text = "Solo te faltaba aguatar "+_gm.TiempoRestante+" segundos mas";
            _comentario.color = Color.red;
            _comentario.text = "GG (get good)";
        }
    }

    public void CargarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }
}
