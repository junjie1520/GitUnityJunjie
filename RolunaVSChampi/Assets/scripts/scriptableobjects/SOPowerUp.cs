using UnityEngine;

[CreateAssetMenu(fileName = "SOPowerUp", menuName = "Scriptable Objects/SOPowerUp")]
public class SOPowerUp : ScriptableObject
{
    [SerializeField]
    private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField]
    private string _tag;
    public string Tag => _tag;
}
