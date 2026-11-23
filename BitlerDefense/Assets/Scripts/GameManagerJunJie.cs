using System.Collections;
using UnityEngine;

public class GameManagerJunJie : MonoBehaviour
{
    [SerializeField]
    private int _money;
    private int MinesL1;
    private int MinesL2;
    private int MinesL3;
    void Start()
    {
        MinesL1 = 0;
        MinesL2 = 0;
        MinesL3 = 0;
        IniciarJuego();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void IniciarJuego()
    {
        StartCoroutine(AddMoney());
    }

    private IEnumerator AddMoney()
    {
        _money += 5 + (MinesL1*5) + (MinesL2*6) + (MinesL3*7);
        yield return new WaitForSeconds(3f);
        StartCoroutine(AddMoney());
    }

    public bool BuyTurret(int value)
    {
        if (_money - value < 0)
        {
            return false;
        }
        else
        {
            _money -= value;
            return true;
        }
    }
}
