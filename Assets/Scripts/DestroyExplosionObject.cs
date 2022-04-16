using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExplosionObject : MonoBehaviour
{
	private float _duration;
	private float _elapsedTime;

	private void Start()
	{
		var main = GetComponent<ParticleSystem>().main;
		_duration = main.duration + main.startLifetime.constant;
	}

	private void Update()
	{
		if (_elapsedTime >= _duration)
			Destroy(gameObject);

		_elapsedTime += Time.deltaTime;
	}
}
