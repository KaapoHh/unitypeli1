using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;
    public float minDistanceFromCampfire = 15f;

    private Transform campfireTransform;

    void Start()
    {
        GameObject campfireObj = GameObject.FindWithTag("Campfire");
        if (campfireObj != null)
        {
            campfireTransform = campfireObj.transform;
        }

        InvokeRepeating("SpawnEnemy", 2f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null) return;

        float randomX = Random.Range(-30f, 60f);
        float spawnY = 0f;

        if (campfireTransform != null)
        {
            float campfireX = campfireTransform.position.x;

            if (Mathf.Abs(randomX - campfireX) < minDistanceFromCampfire)
            {
                int direction = Random.value > 0.5f ? 1 : -1;
                randomX = campfireX + (minDistanceFromCampfire * direction);
            }

            spawnY = campfireTransform.position.y;
        }

        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}