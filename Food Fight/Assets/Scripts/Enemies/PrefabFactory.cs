using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabFactory : MonoBehaviour
{
    public GameObject EnemyAPrefab;
    public GameObject EnemyBPrefab;

    private GameObject enemyToSpawn;

    public GameObject CreateEnemy(string type)
    {
        enemyToSpawn = null;

        if (type.Equals("EnemyA"))
        {
            enemyToSpawn = EnemyAPrefab;
        }
        else if (type.Equals("EnemyB"))
        {
            enemyToSpawn = EnemyBPrefab;
        }

        return enemyToSpawn;
    }
}
