
using UnityEngine;

[CreateAssetMenu(fileName = "SOPowerUp", menuName = "Scriptable Objects/SOPowerUp")]
public class SOPowerUp : ScriptableObject
{
    public enum PowerUpType { Invencible, Curacio, Nitro, Berserker }

    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField] private PowerUpType _type;
    public PowerUpType Type => _type;

    [SerializeField] private int _vida;
    [SerializeField] private float _velocidad;
    [SerializeField] private float _salto;
    [SerializeField] private bool _corutina;
    [SerializeField] private float _duracion;
    [SerializeField] private float _cooldown;

    public int Vida => _vida;
    public float Velocidad => _velocidad;
    public float Salto => _salto;
    public bool Corutina => _corutina;
    public float Duracion => _duracion;
    public float Cooldown => _cooldown;
}
