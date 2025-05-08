using UnityEngine;
using TMPro;

public class ER_GameManager : MonoBehaviour
{

    public static ER_GameManager instance;
    [SerializeField] private GameObject[] lifeBarHeart;
     private int _playerLife;

    // Game Over
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TMP_Text _currentScore;
    [SerializeField] private TMP_Text _highScore;


    void Awake(){
        instance = this;
        _playerLife = lifeBarHeart.Length;
        Time.timeScale = 1;
        _gameOverPanel.SetActive(false);
    }

    void Update(){

        if(_playerLife <= 0){
            GameOver();
        }
    }

    public void LoseLife(){

        if (_playerLife > 0)
        {
            _playerLife--;
            Destroy(lifeBarHeart[_playerLife]);
            lifeBarHeart[_playerLife] = null; 
        }

    }

    private void GameOver(){
        Time.timeScale = 0;
        _gameOverPanel.SetActive(true);
        int currentScore = Mathf.FloorToInt(ER_PlayerMove.instance.GetScore());
        int savedHighScore = PlayerPrefs.GetInt("ERGameHighScore", 0);

        if (_currentScore != null)
        {
            _currentScore.text = "Score: " + currentScore;
        }

        if (currentScore > savedHighScore)
        {
            PlayerPrefs.SetInt("ERGameHighScore", currentScore);
            PlayerPrefs.Save();
        }

        int finalHighScore = PlayerPrefs.GetInt("ERGameHighScore", 0);

        if (_highScore != null)
        {
            _highScore.text = "High Score: " + finalHighScore;
        }

    }


}
