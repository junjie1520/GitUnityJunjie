using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class NodeBehaviour : MonoBehaviour
{
    private List<String> _directions = new List<string>();

    void Start()
    {
        Tilemap _tilemap = GetComponentInParent<Tilemap>();
        Vector3Int _tile = _tilemap.WorldToCell(transform.position);
        if (_tilemap.HasTile(_tile + Vector3Int.up))
        {
            _directions.Add("UP");
        }
        if (_tilemap.HasTile(_tile + Vector3Int.down))
        {
            _directions.Add("DOWN");
        }
        if (_tilemap.HasTile(_tile + Vector3Int.left))
        {
            _directions.Add("LEFT");
        }
        if (_tilemap.HasTile(_tile + Vector3Int.right))
        {
            _directions.Add("RIGHT");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController _enemy = collision.GetComponent<EnemyController>();
        List<string> _options = new List<string>();
        string _origin = Opposite(_enemy.Direction);
        foreach (string _dir in _directions)
        {
            if (_dir != _origin)
            {
                _options.Add(_dir);
            }
        }
        _enemy.SetNextDirection(_options[UnityEngine.Random.Range(0,_options.Count)],transform.position);
    }

    private string Opposite(string direction)
{
    if (direction == "UP")
    {
        return "DOWN";
    }
    else if (direction == "DOWN")
    {
        return "UP";
    }
    else if (direction == "LEFT")
    {
        return "RIGHT";
    }
    else
    {
        return "LEFT";
    }
}
}
