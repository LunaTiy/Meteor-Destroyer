using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
	[SerializeField] private GameObject _meteorPrefab;
    [SerializeField] private float _minSpawnDelay = 1f;
    [SerializeField] private float _maxSpawnDelay = 3f;
	[Space]
	[SerializeField] private float _complicator = 1.1f;

	private float _spawnXLimit;

	private float _minSpawnDelayOnStart;
	private float _maxSpawnDelayOnStart;
	private float _complicatorOnStart;


	private void Start()
	{
		_spawnXLimit = Camera.main.orthographicSize;

		_minSpawnDelayOnStart = _minSpawnDelay;
		_maxSpawnDelayOnStart = _maxSpawnDelay;
		_complicatorOnStart = _complicator;

		SpawnMeteor();
	}

	private void SpawnMeteor()
	{
		float randomPositionX = Random.Range(-_spawnXLimit, _spawnXLimit);
		Vector3 spawnPosition = transform.position + new Vector3(randomPositionX, 0);

		Instantiate(_meteorPrefab, spawnPosition, Quaternion.identity, transform);

		Invoke("SpawnMeteor", Random.Range(_minSpawnDelay, _maxSpawnDelay));
	}

	private void DestroyMeteors()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
	}

	private void ResetParameters()
	{
		_minSpawnDelay = _minSpawnDelayOnStart;
		_maxSpawnDelay = _maxSpawnDelayOnStart;
		_complicator = _complicatorOnStart;
	}

	public void SpawnIncrease()
	{
		if (_minSpawnDelay > 0.2f && _maxSpawnDelay > 0.6f)
		{
			_minSpawnDelay /= _complicator;
			_maxSpawnDelay /= _complicator;

			_complicator *= _complicator;
		}
	}

	public void ReloadGame()
	{
		DestroyMeteors();
		ResetParameters();
	}
}
