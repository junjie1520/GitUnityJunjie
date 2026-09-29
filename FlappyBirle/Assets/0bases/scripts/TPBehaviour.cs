using System;
using Unity.VisualScripting;
using UnityEngine;

public class TPBehaviour : MonoBehaviour
{
    public float _separacion;
    void OnCollisionEnter2D(Collision2D collision)
    {

        string _tag = collision.gameObject.tag;

        GameObject[] _palos = GameObject.FindGameObjectsWithTag(_tag);

        float _Y = UnityEngine.Random.Range(-2.5f, 2.5f);
        int _doble = UnityEngine.Random.Range(1,11);
        foreach (GameObject _palo in _palos)
        {
            if (_doble == 1)
            {
                _palo.GetComponent<SpriteRenderer>().color = Color.softRed;
                _separacion = 7.5f;
            }
            else
            {
                _palo.GetComponent<SpriteRenderer>().color = Color.blue;
                _separacion = 8f;
            }
            Rigidbody2D _rbp = _palo.GetComponent<Rigidbody2D>();
            float _lado;
            if (_rbp.position.y > 0)
            {
                _lado = 1f;
            }
            else
            {
                _lado = -1f;
            }
            _rbp.position = new Vector2(18, _Y + _separacion * _lado);
        }
    }   
}
