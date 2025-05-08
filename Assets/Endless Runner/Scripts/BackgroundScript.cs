using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
   
   [SerializeField] private float _initialSpeed = 0.1f;
    [SerializeField] private float _maxSpeed = 1f;
    [SerializeField] private float _acceleration = 0.01f;
    [SerializeField] private Renderer _bgRend;

    private float _currentSpeed;

    void Start()
    {
        _currentSpeed = _initialSpeed;
    }

    void Update()
    {
        _currentSpeed = Mathf.Min(_currentSpeed + _acceleration * Time.deltaTime, _maxSpeed);
        _bgRend.material.mainTextureOffset += new Vector2(_currentSpeed * Time.deltaTime, 0);
    }
}
