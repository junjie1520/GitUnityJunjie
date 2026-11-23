using UnityEngine;

[CreateAssetMenu(fileName = "SOConstruction", menuName = "Scriptable Objects/SOConstruction")]
public class SOConstruction : ScriptableObject
{
    public enum ConstructionType { Projectil, Laser, Bomb, Mine }
    [SerializeField] private Sprite _sprite1;
    public Sprite Sprite1 => _sprite1;
    [SerializeField] private Sprite _sprite2;
    public Sprite Sprite2 => _sprite2;
    [SerializeField] private Sprite _sprite3;
    public Sprite Sprite3 => _sprite3;

    [SerializeField] private ConstructionType _type;
    public ConstructionType Type => _type;

    [SerializeField] private SOAtack _soAtack;
    public SOAtack soAtack => _soAtack;

    [SerializeField] private int _damage;
    public int Damage => _damage;
    [SerializeField] private float _cooldown;   
    public float Cooldown => _cooldown;
    [SerializeField] private int _value;
    public int Value => _value;
}
