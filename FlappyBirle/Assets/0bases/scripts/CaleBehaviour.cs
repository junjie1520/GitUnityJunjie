using System;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine;

public class CaleBehaviour : MonoBehaviour
{
    private PuntuacionManager _puntuacionManager;
    private Rigidbody2D _cale;
    private int _HP = 2;
    private bool _collision = false;
    private Transform _birle;

    public event Action<int> OnCaleDestroy;
    void Awake()
    {
        _cale = GetComponent<Rigidbody2D>();
    }

    public void SetBirle(Transform _a)
    {
        _birle = _a;
    }

    private void OnMouseDown()
    {
        _HP--;
        if (_HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "birle")
        {
            _collision = true;
        }
    }

    void Update()
    {
        if (_birle == null || _collision == true)
        {
            return;
        }
        float _x = 0f;
        float _y = 0f;
        if (_cale.position.x < _birle.position.x)
        {
            _x = 0.01f;
        }else if (_cale.position.x > _birle.position.x)
        {
            _x = -0.01f;
        }
        if (_cale.position.y < _birle.position.y)
        {
            _y = 0.0005f;
        }else if (_cale.position.y > _birle.position.y)
        {
            _y = -0.0005f;
        }

        _cale.position = new Vector2(_cale.position.x+_x,_cale.position.y+_y);
    }
}
