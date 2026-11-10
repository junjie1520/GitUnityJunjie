using UnityEngine;

[CreateAssetMenu(fileName = "SOEnemigos", menuName = "Scriptable Objects/SOEnemigos")]
public class SOEnemigos : ScriptableObject
{
    public enum EnemyType {Normal, Tanque, Rapido, Fantasma}
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField] private EnemyType _type;
    public EnemyType Type => _type;

    [SerializeField] private int _id;
    [SerializeField] private int _vida;
    [SerializeField] private float _velocidad;
    [SerializeField] private int _daño;
   
    public int Id => _id;
    public int Vida => _vida;
    public float Velocidad => _velocidad;
    public float Daño => _daño;
    
}
