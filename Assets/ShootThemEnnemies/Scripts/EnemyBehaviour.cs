using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _rotateSpeed = 0.025f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!_player)
        {
            GetTarget();
        }
        else
        {
            RotateTowardsTarget();
        }
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = transform.up * _speed; 
    }

    private void GetTarget()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj)
        {
            _player = playerObj.transform;
        }
    }

    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = _player.position - transform.position;
        float rotation = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, rotation));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, _rotateSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GameManager.instance.AddScore(10);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player != null && !player.IsShielded())
            {
                GameManager.instance.PlayerTakeDamage();
                player.FlashRed();
            }
            Destroy(gameObject);
        }
    }

    public void IncreaseSpeed(float amount)
    {
        _speed = Mathf.Min(_speed + amount, 10f);
    }
}
