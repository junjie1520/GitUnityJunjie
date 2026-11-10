using UnityEngine;

[CreateAssetMenu(fileName = "SOEstructura", menuName = "Scriptable Objects/SOEstructura")]
public class SOEstructura : ScriptableObject
{
    public enum EstructuraType { Proyectil, Laser, Bomba, Mina }
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField] private EstructuraType _type;
    public EstructuraType Type => _type;

    [SerializeField] private int _id;
    [SerializeField] private int _daño;
    [SerializeField] private float _cooldown;

    public int Id => _id;
    public float Daño => _daño;
    public float Cooldown => _cooldown;
}
