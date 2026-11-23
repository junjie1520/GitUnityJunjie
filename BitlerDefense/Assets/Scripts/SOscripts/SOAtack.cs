using UnityEngine;

[CreateAssetMenu(fileName = "SOAtack", menuName = "Scriptable Objects/SOAtack")]
public class SOAtack : ScriptableObject
{
    [SerializeField] private float _speed = 10f;
    public float Speed => _speed;   


    [SerializeField] private int _damage;
    public int Damage => _damage;


    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField] private RuntimeAnimatorController _animation;
    public RuntimeAnimatorController Animation => _animation;


}