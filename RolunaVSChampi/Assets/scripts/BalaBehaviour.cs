using UnityEngine;
using System.Collections;

public class BalaBehaviour : MonoBehaviour
{
    private float _velocidad = 15; 
    private int _dany = 1;
    private GameObject _rolunaPrefab;
    public void Disparar(Vector3 direccio, int dany)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        _dany = dany;
        rb.linearVelocity = direccio*_velocidad;
    }

    void Awake()
    {
        StartCoroutine(destroy());
    }

    private IEnumerator destroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    public int dany
    {
        get {return _dany;}
    }

}
