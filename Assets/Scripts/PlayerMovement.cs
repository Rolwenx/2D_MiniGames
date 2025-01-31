using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float INITIAL_SPEED = 8.0f; 
    private const float ACCELERATION = 10.0f;
    private const float DECELERATION = 10.0f;

    [SerializeField] private Rigidbody2D _rb;

    private Vector2 _movementInput;
    private Vector2 _currentVelocity;
    private float _speed;

    void Start(){
        _speed = INITIAL_SPEED;
    }

    void Update(){
        HandleInput();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    private void HandleInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

       _movementInput = new Vector2(moveX, moveY);
    }

    private void ApplyMovement()
    {
        _currentVelocity = Vector2.Lerp(_currentVelocity, _movementInput * _speed, Time.fixedDeltaTime * (_movementInput.magnitude > 0 ? ACCELERATION : DECELERATION));
        _rb.velocity = _currentVelocity;
    }
    
}
