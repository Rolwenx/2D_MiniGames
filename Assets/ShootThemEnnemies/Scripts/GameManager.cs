using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private int _playerLives = 5;
    [SerializeField] private Image[] lifeImages;  

    [SerializeField] private TMP_Text _scoreText;

    // Game Over
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private TMP_Text _highScoreText;
    private int _score = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        Time.timeScale = 1;
        _gameOverPanel.SetActive(false);
    }
    
    public void AddScore(int points)
    {
        _score += points;
        if (_scoreText != null)
        {
            _scoreText.text = "Score: " + _score;
        }
    }

    public void PlayerTakeDamage()
    {
        _playerLives--;
        UpdateLifeUI();

        if (_playerLives <= 0)
        {
            GameOver();
        }
    }

    private void UpdateLifeUI()
    {
        if (_playerLives >= 0 && _playerLives < lifeImages.Length)
        {
            lifeImages[_playerLives].enabled = false;
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        _gameOverPanel.SetActive(true);

        int savedHighScore = PlayerPrefs.GetInt("ShootGameHighScore", 0);

        if (_currentScoreText != null)
        {
            _currentScoreText.text = "Score: " + _score;
        }

        if (_score > savedHighScore)
        {
            PlayerPrefs.SetInt("ShootGameHighScore", _score);
            PlayerPrefs.Save();
        }

        int finalHighScore = PlayerPrefs.GetInt("ShootGameHighScore", 0);

        if (_highScoreText != null)
        {
            _highScoreText.text = "High Score: " + finalHighScore;
        }
    }
}
