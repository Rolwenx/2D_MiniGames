using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float INITIAL_SPEED = 8.0f; 
    private const float ACCELERATION = 10.0f;
    private const float DECELERATION = 10.0f;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firingPoint;

    [Range(0.1f,2f)]
    [SerializeField] private float _firingRate = 0.5f;

    private float _fireTimer;
    private Vector2 _movementInput;
    private Vector2 _currentVelocity;
    private float _speed;

    void Start(){
        _speed = INITIAL_SPEED;
    }

    void Update(){
        HandleInput();
        ShootBehaviour();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ShootBehaviour(){

        if(Input.GetMouseButtonDown(0) && _fireTimer <= 0f){
            Shoot();
            _fireTimer = _firingRate;
        }
        else{
            _fireTimer -= Time.deltaTime;
        }
    }

    private void Shoot(){
        Instantiate(_bulletPrefab, _firingPoint.position, _firingPoint.rotation);
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
        _rb.linearVelocity = _currentVelocity;
    }
    
}
