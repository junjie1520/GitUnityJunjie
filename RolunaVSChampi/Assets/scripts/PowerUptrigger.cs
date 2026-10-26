using UnityEngine;

public class PowerUptrigger : MonoBehaviour
{
    private RolunaBehaviour _jugador;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _jugador = FindFirstObjectByType<RolunaBehaviour>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           

            Destroy(gameObject);

        }
    }
}
