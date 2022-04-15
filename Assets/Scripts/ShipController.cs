using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipController : MonoBehaviour
{
	[SerializeField] private UnityEvent EventGameOver;
	[SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private float _speed;
	[SerializeField] private float _reloadTime;

	private Vector3 _spawnShipPosition;

	private float _elapsedTime;
	private Vector3 _spawnBulletPosition;

	private float _limitCameraSizeX;
	private float _limitCameraSizeY;

	private void Start()
	{
		_limitCameraSizeX = Camera.main.orthographicSize + transform.localScale.x / 2;
		_limitCameraSizeY = Camera.main.orthographicSize + transform.localScale.y;

		_spawnShipPosition = transform.position;
	}

	private void Update()
	{
		Movement();
		Fire();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy") EventGameOver.Invoke();
	}

	private void Movement()
	{
		float mX = Input.GetAxis("Horizontal") * _speed;
		float mY = Input.GetAxis("Vertical") * _speed;

		Vector2 movement = new Vector2(mX, mY);

		transform.Translate(movement * Time.deltaTime);

		ClampPosition();
	}

	private void Fire()
	{
		_elapsedTime += Time.deltaTime;

		if (Input.GetButtonDown("Jump") && _elapsedTime >= _reloadTime)
		{
			Vector3 spawnBulletPosition = transform.position - new Vector3(0, transform.localScale.y, 0);

			Instantiate(_bulletPrefab, spawnBulletPosition, Quaternion.identity);
			_elapsedTime = 0f;
		}
	}

	private void ClampPosition()
	{
		Vector3 position = transform.position;
		position.x = Mathf.Clamp(position.x, -_limitCameraSizeX, _limitCameraSizeX);
		position.y = Mathf.Clamp(position.y, -_limitCameraSizeY, _limitCameraSizeY);

		transform.position = position;
	}

	public void ResetShipPosition()
	{
		transform.position = _spawnShipPosition;
	}
}
