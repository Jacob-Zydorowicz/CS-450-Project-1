using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyFactory factory;
    public Enemy enemy;

    public EnemySpawner(EnemyFactory factory)
    {
        this.factory = factory;
    }

    public Enemy SpawnEnemy(string type)
    {
        enemy = factory.CreateEnemy(type);

        return enemy;
    }

}
