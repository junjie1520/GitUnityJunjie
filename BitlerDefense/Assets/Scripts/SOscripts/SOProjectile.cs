using UnityEngine;

[CreateAssetMenu(fileName = "SOProjectile", menuName = "Scriptable Objects/SOProjectile")]
public class SOProjectile : ScriptableObject
{
    [SerializeField] private float _speed = 10f;
    public float Speed => _speed;


    [SerializeField] private int _damage;
    public int Damage => _damage;
}