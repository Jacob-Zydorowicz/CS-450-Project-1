using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveSystem : MonoBehaviour
{

    int wave = 1;
    List<GameObject> enemies;
    GameSubject gs;
    bool change = false;
    [SerializeField] string[] enemyArray;
    [SerializeField] string level;
    [SerializeField] Vector2[] spawnPoints;
    [SerializeField] float spawnBuffer;
    EnemySpawner es;

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
            SceneManager.LoadScene(level);
        }
        for (int i = 0; i < 1 + wave * 2; i++)
        {
            string enemyType = enemyArray[Random.Range(0, enemyArray.Length)];
            Vector2 spawnPos = GetRandomPos();
            while((spawnPos-(Vector2)GameObject.FindGameObjectWithTag("Player").transform.position).magnitude<spawnBuffer)
            {
                spawnPos = GetRandomPos();
            }
            GameObject enemyToSpawn = es.SpawnEnemy(enemyType);
            Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);
        }
    }

    Vector2 GetRandomPos()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
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
