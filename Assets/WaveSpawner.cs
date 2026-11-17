using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    [Header("Vihollisuunnitelmat (Prefabit)")]
    public GameObject normalEnemyPrefab;
    public GameObject fastEnemyPrefab;

    [Header("Spawn-paikat")]
    public Transform[] spawnPoints; 

    [Header("Aallon asetukset")]
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    private int waveNumber = 0;

    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        waveNumber++;
        Debug.Log("Aloitetaan aalto numero: " + waveNumber);

        int enemyCount = waveNumber * 3;

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f); 
        }
    }

    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemyToSpawn = normalEnemyPrefab;

        if (waveNumber >= 2 && Random.value < 0.3f) 
        {
            enemyToSpawn = fastEnemyPrefab;
        }

        Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}