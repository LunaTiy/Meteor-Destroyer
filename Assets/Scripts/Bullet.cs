using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

	[SerializeField] private GameManager _gameManager;
	private Rigidbody2D _rb;

	private void Start()
	{
		_gameManager = FindObjectOfType<GameManager>();

		_rb = GetComponent<Rigidbody2D>();
		_rb.velocity = new Vector2(0f, _speed);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Enemy")
		{
			Destroy(collision.gameObject);
			_gameManager.AddScore();
			Destroy(gameObject);
		}
	}
}
