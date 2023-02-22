    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public Enemy CreateEnemy(string type)
    {
        Enemy enemy = null;

        if (type.Equals("EnemyA"))
        {
            enemy = new EnemyA();
        }
        else if (type.Equals("EnemyB"))
        {
            enemy = new EnemyB();
        }

        return enemy;
    }
}
