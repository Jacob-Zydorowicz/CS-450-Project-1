using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyFactory scriptFactory;
    public PrefabFactory prefabFactory;
    private GameObject enemy;
    public WaveSystem waveSystem;

    //public EnemySpawner(EnemyFactory factory)
    //{
    //    this.factory = factory;
    //}

    public GameObject SpawnEnemy(string type)
    {
        enemy = prefabFactory.CreateEnemy(type);

        scriptFactory.AddEnemyScript(enemy, type);

        waveSystem.Add(enemy);

        return enemy;
    }

    private void OnDestroy()
    {
        waveSystem.Remove(enemy);
    }
}
