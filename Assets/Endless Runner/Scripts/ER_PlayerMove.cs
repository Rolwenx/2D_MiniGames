using UnityEngine;

public class ER_PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private Rigidbody2D _rb;

    void Start(){

        _rb = GetComponent<Rigidbody2D>();
    }

    void Update(){

        float moveY = Input.GetAxis("Vertical");
        _rb.linearVelocity = new Vector2(0,moveY * _speed);
    }
}
