using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _rotateSpeed = 0.025f;



    void Update()
    {
        // Get the target's position
        if (!_player){
            GetTarget();
        }
        else{
            // Rotate towards the target
            RotateTowardsTarget();
        }
    }

    void FixedUpdate()
    {
        // Move towards the target
        _rb.velocity = transform.up * _speed;
        
    }

    private void GetTarget(){
        if(GameObject.FindGameObjectWithTag("Player")){
             _player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void RotateTowardsTarget(){

        Vector2 targetDirection =  _player.position - transform.position;
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
        }
        else if(collision.gameObject.CompareTag("Player")){
            Destroy(collision.gameObject);
        }
    }
}
