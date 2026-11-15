using UnityEngine;
using UnityEngine.UIElements;

public class NodeBehaviour : MonoBehaviour
{
    private Vector3 _pos;
    void Awake()
    {
        _pos = gameObject.transform.position;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (true)
        {
            
        }
    }
}
