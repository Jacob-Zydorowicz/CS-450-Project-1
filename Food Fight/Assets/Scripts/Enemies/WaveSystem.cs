/*
 * Jacob Zydorowicz
 * WaveSystem.cs
 * Food Fight
 * Manages enemy wave system and spawning of enemies.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WaveSystem : MonoBehaviour
{

    int wave = 1;
    List<GameObject> enemies;
    [SerializeField] List<GameObject> spawnPoints;
    GameSubject gs;
    bool change = false;
    [SerializeField] string[] enemyArray;
    [SerializeField] string level;
    [SerializeField] float spawnBuffer;
    EnemySpawner es;

    public UnityEvent<string> levelWon = new UnityEvent<string>();

    // Start is called before the first frame update
    void Start()
    {

        es = gameObject.GetComponent<EnemySpawner>();
        gs = GetComponent<GameSubject>();
        gs.UpdateWave(wave);
        enemies = new List<GameObject>();
        SpawnWave();
    }

    void SpawnWave()
    {
        if(wave>5)
        {
            levelWon.Invoke(level);
        }
        else
        {
            for (int i = 0; i < 1 + wave * 2; i++)
            {

                string enemyType = enemyArray[Random.Range(0, enemyArray.Length)];
                Vector2 spawnPos = GetRandomPos();
                while ((spawnPos - (Vector2)GameObject.FindGameObjectWithTag("Player").transform.position).magnitude < spawnBuffer)
                {
                    spawnPos = GetRandomPos();
                }
                GameObject enemyToSpawn = es.SpawnEnemy(enemyType);
                Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);
            }
        }
    }

    //randomly selects a spawn position from the list and gets its coordinates
    Vector2 GetRandomPos()
    {
        int spawnerNum = Random.Range(0, spawnPoints.Count - 1);
        Vector2 spawnerCoords = new Vector2(spawnPoints[spawnerNum].transform.position.x, spawnPoints[spawnerNum].transform.position.y);
        return spawnerCoords;
    }
    // Update is called once per frame
    void Update()
    {
        if (change)
        {
            gs.UpdateEnemies(enemies.Count);
            change = false;
            if (enemies.Count == 0)
            {
                wave++;
                gs.UpdateWave(wave);
                SpawnWave();
            }
        }
    }

    public void Add(GameObject enemy)
    {
        enemies.Add(enemy);
        change = true;
        
    }

    public void Remove(GameObject enemy)
    {
        enemies.Remove(enemy);
        change = true;
    }
}
