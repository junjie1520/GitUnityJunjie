using UnityEngine;

public class BalaBehaviour : MonoBehaviour
{
    public float velocidad = 15; 


    private GameObject _rolunaPrefab;
    public void Disparar(Vector3 direccio, float velocidad)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direccio*velocidad;
    }

    private void Update()
    {
        if(transform.position.x > 21 || transform.position.x < -21)
        {
            Destroy(gameObject);
        }
    }


}
