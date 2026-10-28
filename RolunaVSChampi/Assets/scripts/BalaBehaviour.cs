using UnityEngine;

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

    private void Update()
    {
        if(transform.position.x > 50 || transform.position.x < -50)
        {
            Destroy(gameObject);
        }
    }

    public int dany
    {
        get {return _dany;}
    }


}
