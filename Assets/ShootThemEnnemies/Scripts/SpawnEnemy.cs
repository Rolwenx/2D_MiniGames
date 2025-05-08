using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private float initialSpawnRate = 5f;
    [SerializeField] private float minSpawnRate = 1f;
    [SerializeField] private float spawnRateDecrease = 0.2f;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject splitterEnemyPrefab;

    public bool isMasterSpawner = false;
    [SerializeField, Range(0f, 1f)] private float splitterSpawnChance = 0.05f;
    [SerializeField] private float maxSplitterChance = 0.15f;

    private float currentSpawnRate;
    private float timer;
    private bool canSpawn = true;

    private static SpawnEnemy[] allSpawners;

    void Start()
    {
        currentSpawnRate = initialSpawnRate;

        if (isMasterSpawner)
        {
            allSpawners = FindObjectsByType<SpawnEnemy>(FindObjectsSortMode.None);
            StartCoroutine(MasterSpawner());
        }
    }

    void Update()
    {
        if (!isMasterSpawner) return;

        timer += Time.deltaTime;

        if (timer >= 15f)
        {
            timer = 0f;
            currentSpawnRate = Mathf.Max(minSpawnRate, currentSpawnRate - spawnRateDecrease);
            splitterSpawnChance = Mathf.Min(maxSplitterChance, splitterSpawnChance + 0.02f);

            EnemyBehaviour[] enemies = FindObjectsByType<EnemyBehaviour>(FindObjectsSortMode.None);
            foreach (var enemy in enemies)
            {
                enemy.IncreaseSpeed(0.1f);
            }
        }
    }

    private IEnumerator MasterSpawner()
    {
        while (canSpawn)
        {
            yield return new WaitForSeconds(currentSpawnRate);

            int numSpawners = Random.Range(1, allSpawners.Length + 1); // 1–N spawners
            List<SpawnEnemy> spawnerList = new List<SpawnEnemy>(allSpawners);
            ShuffleList(spawnerList);

            for (int i = 0; i < numSpawners; i++)
            {
                spawnerList[i].SpawnOne();
            }
        }
    }

    public void SpawnOne()
    {
        float chance = Random.value;

        if (chance < splitterSpawnChance)
        {
            Instantiate(splitterEnemyPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            int rand = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[rand], transform.position, Quaternion.identity);
        }
    }

    private void ShuffleList(List<SpawnEnemy> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);
            var temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}
