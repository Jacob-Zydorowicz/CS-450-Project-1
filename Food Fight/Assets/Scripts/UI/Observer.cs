using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Observer
{
    public void statUpdate(int health, int wave, int enemyCount, int bulletCount, string currentWeapon);
}
