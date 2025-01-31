using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Range(0f, 10f)]
    [SerializeField] private float _speed = 10f;

    [Range(0f, 10f)]
    [SerializeField] private float _timeLife = 3f;

    [SerializeField] private Rigidbody2D _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject,_timeLife);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.velocity = transform.up * _speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
