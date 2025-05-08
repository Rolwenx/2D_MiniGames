using UnityEngine;
using TMPro;

public class ER_PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private Rigidbody2D _rb;


    // Score Management
    private float _distanceScore = 0f;
    [SerializeField] private TMP_Text scoreText;
    public static ER_PlayerMove instance;

    void Awake(){
        instance = this;
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
    }

    void Update(){

        float moveY = Input.GetAxis("Vertical");
        _rb.linearVelocity = new Vector2(0,moveY * _speed);

        _distanceScore += Time.deltaTime * 10f;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(_distanceScore).ToString();
        }
    }

    public float GetScore()
    {
        return Mathf.FloorToInt(_distanceScore);
    }

    public void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Enemy")){
            ER_GameManager.instance.LoseLife();
            Destroy(other.gameObject);
        }
    }
}
