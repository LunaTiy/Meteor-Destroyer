using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 2f;
    [SerializeField] private float minY = -20f;
    [SerializeField] private float maxY = 40f;

	private void Update()
	{
		Movement();
	}

	private void Movement()
	{
		transform.Translate(0, -_scrollSpeed * Time.deltaTime, 0);

		if (transform.position.y <= minY)
			transform.Translate(0, maxY, 0);
	}

	public void BoostScroll()
	{
		_scrollSpeed += 0.2f;
	}
}
