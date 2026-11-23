using UnityEngine;

[CreateAssetMenu(fileName = "SOConstruction", menuName = "Scriptable Objects/SOConstruction")]
public class SOConstruction : ScriptableObject
{
    public enum ConstructionType { Projectil, Laser, Bomb, Mine }
    [SerializeField] private int _level = 1;
    public int Level => _level;
    [SerializeField] private SOConstruction _nextLevel;
    public SOConstruction NextLevel => _nextLevel;

    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField] private ConstructionType _type;
    public ConstructionType Type => _type;

    [SerializeField] private SOAtack _soAtack;
    public SOAtack soAtack => _soAtack;


    [SerializeField] private int _id;
    public int Id => _id;
    [SerializeField] private int _damage;
    public float Damage => _damage;
    [SerializeField] private float _cooldown;   
    public float Cooldown => _cooldown;
}
