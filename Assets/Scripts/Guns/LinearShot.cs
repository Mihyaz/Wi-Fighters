using UnityEngine;

public class LinearShot : IShot
{
    private int _spreadCount;
    private float _speed;

    public LinearShot(int SpreadCount, float Speed)
    {
        _spreadCount = SpreadCount;
        _speed = Speed;
    }

    public void Fire(Bullet _bullet, Transform firePointTransform)
    {
        for (int i = 0; i < _spreadCount; i++)
        {
            Bullet bullet = GameObject.Instantiate(_bullet, firePointTransform.position, firePointTransform.rotation) as Bullet;
            bullet.Rigidbody.velocity = firePointTransform.up * _speed;
        }
    }
}
