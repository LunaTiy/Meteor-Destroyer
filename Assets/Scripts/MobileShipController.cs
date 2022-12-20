using UnityEngine;

public class MobileShipController : ShipController
{
    protected override void Movement()
    {
        float mX = Input.acceleration.x * _speed;
        float mY = Input.acceleration.y * _speed;

        var movement = new Vector2(mX, mY);

        transform.Translate(movement * Time.deltaTime);

        ClampPosition();
    }

    protected override void Fire()
    {
        _elapsedTime += Time.deltaTime;

        if (!(Input.touchCount > 0) || !(_elapsedTime >= _reloadTime)) return;
        Vector3 spawnBulletPosition = transform.position - new Vector3(0, transform.localScale.y, 0);

        Instantiate(_bulletPrefab, spawnBulletPosition, Quaternion.identity);
        _elapsedTime = 0f;
    }
}