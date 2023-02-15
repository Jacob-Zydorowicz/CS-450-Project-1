using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    GameSubject gs;
    // Start is called before the first frame update
    void Start()
    {
        gs = FindObjectOfType<GameSubject>();
        gs.UpdateHealth(100);
        gs.UpdateWave(1);
        gs.UpdateEnemies(10);
        gs.UpdateWeapon("Grape Shot");
        gs.UpdateAmmo(60);
    }
}


