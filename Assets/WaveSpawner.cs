using UnityEngine;
using System.Collections;
using TMPro; 

public class WaveSpawner : MonoBehaviour
{
    [Header("Vihollisuunnitelmat (Prefabit)")]
    public GameObject normalEnemyPrefab;
    public GameObject fastEnemyPrefab;

    [Header("Spawn-paikat")]
    public Transform[] spawnPoints;
   
    [Header("Aallon asetukset")]
    public float timeBetweenWaves = 10f; 
    public float spawnInterval = 2f;    
    public int startingEnemies = 2;     
    public int enemiesIncreasePerWave = 1; 
    public int maxEnemiesPerWave = 10;   

    [Header("UI / Peliin näkyvä teksti")]
    public TMP_Text waveText;

    private float countdown = 3f;
    private int waveNumber = 0;
    private bool isSpawning = false;

    void Start()
    {
        UpdateWaveUI();
    }

    void Update()
    {
        if (isSpawning) return;

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        isSpawning = true;
        waveNumber++;
        UpdateWaveUI();
        Debug.Log("Aloitetaan aalto numero: " + waveNumber);

        int enemyCount = Mathf.Min(
            startingEnemies + (waveNumber - 1) * enemiesIncreasePerWave,
            maxEnemiesPerWave
        );

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
    }

    void SpawnEnemy()
    {
        if (spawnPoints == null || spawnPoints.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyToSpawn = normalEnemyPrefab;

        if (fastEnemyPrefab != null && waveNumber >= 2 && Random.value < 0.25f)
        {
            enemyToSpawn = fastEnemyPrefab;
        }

        Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
    }

    void UpdateWaveUI()
    {
        if (waveText != null)
        {
            waveText.text = "Aalto: " + waveNumber;
        }
    }
}