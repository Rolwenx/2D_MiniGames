using UnityEngine;

public class SplitterEnemy : MonoBehaviour
{
    [SerializeField] private GameObject miniEnemyPrefab;
    [SerializeField] private int splitCount = 2;
    [SerializeField] private float splitForce = 2f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            SplitIntoMinis();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.PlayerTakeDamage();
            Destroy(gameObject);
        }
    }

    private void SplitIntoMinis()
    {
        for (int i = 0; i < splitCount; i++)
        {
            Vector2 randomDir = Random.insideUnitCircle.normalized;
            GameObject mini = Instantiate(miniEnemyPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = mini.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(randomDir * splitForce, ForceMode2D.Impulse);
            }
        }
    }
}
