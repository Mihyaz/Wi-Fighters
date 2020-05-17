using UnityEngine;

public class SpreadShot : IShot
{
    private int _angle = -30;
    private int _angleBetweenBullets = 15;
    private int _spreadCount;
    private float _speed;

    public SpreadShot(int SpreadCount, float Speed)
    {
        _spreadCount = SpreadCount;
        _speed = Speed;
    }

    public void Fire(Bullet _bullet, Transform firePointTransform)
    {
        for (int i = 0; i < _spreadCount; i++)
        {
            Bullet bullet = GameObject.Instantiate(_bullet, firePointTransform.position, firePointTransform.rotation) as Bullet;
            bullet.transform.Rotate(bullet.transform.rotation.x, bullet.transform.rotation.y, _angle + (i * _angleBetweenBullets));
            bullet.Rigidbody.velocity = bullet.transform.up * _speed;
        }
    }
}