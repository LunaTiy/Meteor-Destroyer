using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField] private UnityEvent IncreasedDifficulty;
	[SerializeField] private UnityEvent ReloadedGame;

    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _gameOverText;
	[SerializeField] private GameObject _buttonStart;

	[SerializeField] private int _scoreForIncreasedDifficulty = 10;
	private int _currentScore;

    private int _playerScore;

	private void Awake()
	{
		_buttonStart.SetActive(true);
		Time.timeScale = 0f;
	}

	private void Start()
	{
		_gameOverText.enabled = false;
	}

	private void DeleteBullets()
	{
		foreach (var bullet in FindObjectsOfType<Bullet>())
			Destroy(bullet.gameObject);
	}

	private void ResetParameters()
	{
		_buttonStart.SetActive(false);

		_currentScore = 0;
		_playerScore = 0;
		_scoreText.text = $"Score: {_playerScore}";

		_gameOverText.enabled = false;
	}

	public void AddScore()
	{
        _playerScore++;
		_currentScore++;

		if(_currentScore >= _scoreForIncreasedDifficulty)
		{
			IncreasedDifficulty.Invoke();
			_currentScore = 0;
		}

        _scoreText.text = $"Score: {_playerScore}";
	}

    public void GameOver()
	{
		_buttonStart.SetActive(true);
        _gameOverText.enabled = true;
        Time.timeScale = 0;
	}

	public void StartNewGame()
	{		
		DeleteBullets();
		ResetParameters();
		ReloadedGame.Invoke();

		Time.timeScale = 1f;
	}
}
