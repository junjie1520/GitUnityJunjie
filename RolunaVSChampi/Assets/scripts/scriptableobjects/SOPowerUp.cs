using UnityEngine;

[CreateAssetMenu(fileName = "SOPowerUp", menuName = "Scriptable Objects/SOPowerUp")]
public class SOPowerUp : ScriptableObject
{
    public enum PowerUpType {Invencible, Curacio, Nitro, Berserker}
    [SerializeField]
    private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField]
    private PowerUpType _type;
    public PowerUpType Type => _type;
}
