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

    // Pause
    [SerializeField] private GameObject _pausePanel;
    private bool _isPaused = false;



    void Awake(){
        instance = this;
        _playerLife = lifeBarHeart.Length;
        Time.timeScale = 1;
        _gameOverPanel.SetActive(false);
        _pausePanel.SetActive(false);
    }

    void Update(){

        if(_playerLife <= 0){
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
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

    private void TogglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            Time.timeScale = 0;
            _pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            _pausePanel.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _pausePanel.SetActive(false);
        _isPaused = false;
    }

}
