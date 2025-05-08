using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private float initialSpawnInterval = 2f;
    [SerializeField] private float minY = -4f;
    [SerializeField] private float maxY = 4f;
    [SerializeField] private float obstacleLifetime = 10f;
    [SerializeField] private float spawnX = 10f;
    [SerializeField] private float initialObstacleSpeed = 5f;

    private float _spawnInterval;
    private float _obstacleSpeed;
    private float _timer;

    private void Start()
    {
        _spawnInterval = initialSpawnInterval;
        _obstacleSpeed = initialObstacleSpeed;
        InvokeRepeating(nameof(SpawnObstacle), 1f, _spawnInterval);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        // Every 10 seconds, the difficulty will increase
        if (_timer >= 10f)
        {
            _timer = 0f;

            // Decrease spawn interval, but not below 0.5 sec
            _spawnInterval = Mathf.Max(0.5f, _spawnInterval - 0.2f);

            // Increase obstacle speed, but not above 15
            _obstacleSpeed = Mathf.Min(15f, _obstacleSpeed + 1f);

            // Cancel and restart InvokeRepeating with new spawn rate
            CancelInvoke(nameof(SpawnObstacle));
            InvokeRepeating(nameof(SpawnObstacle), 0f, _spawnInterval);
        }
    }

    private void SpawnObstacle()
    {
        int index = Random.Range(0, obstaclePrefabs.Length);
        GameObject obstacle = Instantiate(obstaclePrefabs[index]);

        float randomY = Random.Range(minY, maxY);
        obstacle.transform.position = new Vector2(spawnX, randomY);

        Rigidbody2D rb = obstacle.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = obstacle.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.left * _obstacleSpeed;

        Destroy(obstacle, obstacleLifetime);
    }
}
