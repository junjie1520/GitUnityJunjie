using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WaveManager _waveManager;
    [SerializeField] private BaseBehaviour _base;
    [SerializeField] private TurretPlacementManager _tpManager;
    [SerializeField] //Luego se quita, ahora es para ver el dinero
    private int _money;
    private int MinesL1;
    private int MinesL2;
    private int MinesL3;
    private bool _levelEnded;
    
    void Start()
    {
        MinesL1 = 0;
        MinesL2 = 0;
        MinesL3 = 0;
        IniciarJuego();
    }
    void OnEnable() 
    {
        _waveManager.OnLevelComplete += LevelEnd; 
        _base.OnBaseDestroyed += LevelEnd;
        _tpManager.OnMinePlaceOrUpgrade += MinePlacedOrUpgraded;
    }
    void OnDisable() 
    { 
        _waveManager.OnLevelComplete -= LevelEnd; 
        _base.OnBaseDestroyed -= LevelEnd;
        _tpManager.OnMinePlaceOrUpgrade -= MinePlacedOrUpgraded;
    }

    private void IniciarJuego()
    {
        _waveManager.StartLevel();
        StartCoroutine(AddMoney());
    }

    private IEnumerator AddMoney()
    {
        _money += 5 + (MinesL1*5) + (MinesL2*6) + (MinesL3*7);
        yield return new WaitForSeconds(3f);
        StartCoroutine(AddMoney());
    }

    public bool TryBuyTurret(int value)
    {
        if (_money < value) return false;
        _money -= value;
        return true;
    }

    private void LevelEnd(bool win)
    {
        if (_levelEnded) return;
        _levelEnded = true;
        Time.timeScale = 0f;
        Debug.Log("Nivel terminado " + (win ? ", has ganado" : ", has perdido"));
        // Aqui hacer ir a la pantalla de ggs
        StopAllCoroutines();
    }

    private void MinePlacedOrUpgraded(int oldLevel, int newLevel)
    {
        if (oldLevel == 0 && newLevel == 1)
        {
            MinesL1++;
        }else if (oldLevel == 0 && newLevel == 2)
        {
            MinesL2++;
        }else if (oldLevel == 0 && newLevel == 3)
        {
            MinesL3++;
        }else if (oldLevel == 1 && newLevel == 2)
        {
            MinesL1--;
            MinesL2++;
        }else if (oldLevel == 2 && newLevel == 3)
        {
            MinesL2--;
            MinesL3++;
        }else if (oldLevel == 1 && newLevel == 0)
        {
            MinesL1--;
        }else if (oldLevel == 2 && newLevel == 0)
        {
            MinesL2--;
        }else if (oldLevel == 3 && newLevel == 0)
        {
            MinesL3--;
        }
    }
}
