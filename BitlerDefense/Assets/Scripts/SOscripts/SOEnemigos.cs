using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "SOEnemigos", menuName = "Scriptable Objects/SOEnemigos")]
public class SOEnemigos : ScriptableObject
{
    public enum EnemyType {Normal, Tank, Fast, Phantom}
    [SerializeField] private RuntimeAnimatorController _animator;
    public RuntimeAnimatorController AnimatorController => _animator;

    [SerializeField] private EnemyType _type;
    public EnemyType Type => _type;

    [SerializeField] private int _hp;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private string _direction;
  
    public int HP => _hp;
    public float Speed => _speed;
    public int Damage => _damage;
    public string Direction => _direction;


    
}
