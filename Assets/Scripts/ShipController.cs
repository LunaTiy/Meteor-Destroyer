using System;
using UnityEngine;
using UnityEngine.Events;

public class ShipController : MonoBehaviour
{
	[SerializeField] private UnityEvent _eventGameOver;
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
		if (Camera.main == null)
			throw new NullReferenceException();

		float orthographicSize = Camera.main.orthographicSize;
		Vector3 localScale = transform.localScale;
		
		_limitCameraSizeX = orthographicSize + localScale.x / 2;
		_limitCameraSizeY = orthographicSize + localScale.y;

		// ReSharper disable once Unity.InefficientPropertyAccess
		_spawnShipPosition = transform.position;
	}

	private void Update()
	{
		Movement();
		Fire();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy")) _eventGameOver.Invoke();
	}

	private void Movement()
	{
		float mX = Input.GetAxis("Horizontal") * _speed;
		float mY = Input.GetAxis("Vertical") * _speed;

		var movement = new Vector2(mX, mY);

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
