using Boo.Lang;
using System;
using UnityEngine;

public class LinearShot : IShot
{
    private int _spreadCount;
    private float _speed;
    public LinearShot(Gun gun) 
    {
        if (gun is Shotgun) throw new Exception($"Use LinearShot class to construct a {gun.GetType().Name}");

        _spreadCount = gun.SpreadCount;
        _speed = gun.Speed;
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
