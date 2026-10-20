using UnityEngine;

public class Buffos : MonoBehaviour
{
    private GameObject _prefab;
    
  
    void spawn()
    {
        int x = Random.Range(0, 4);
        GameObject powerup = Instantiate(_prefab);
        switch (x)
        {
            case 0:
                powerup.tag = "Invisible";
                powerup.sprite = 
                break;

            case 1:
                powerup.tag = "Invencible";
                
                break;

            case 2:
                powerup.tag = "Speedboost";
               
                break;

            case 3:
                powerup.tag = "Rambo";
              
                break;
        }
    }



}
