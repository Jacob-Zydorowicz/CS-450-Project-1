    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    private GameObject enemyToSpawn;

    public GameObject AddEnemyScript(GameObject prefab, string type)
    {
        enemyToSpawn = prefab;

        if (type.Equals("EnemyA"))
        {
            if (enemyToSpawn.GetComponent<EnemyA>() == null)
            {
                enemyToSpawn.AddComponent<EnemyA>();
            }
        }

        if (type.Equals("EnemyB"))
        {
            if (enemyToSpawn.GetComponent<EnemyB>() == null)
            {
                enemyToSpawn.AddComponent<EnemyB>();
            }
        }

        return enemyToSpawn;
    }
}
