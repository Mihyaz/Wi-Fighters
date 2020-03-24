using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public enum GunClasses
{
    Rifle,
    Shotgun,
    Handgun,
    Laser
}
public abstract class Gun
{
    public float FireRate;
    public float NextFire;
    public float Damage;
    public float Speed;
    public int ClipSize;
    public int Ammo;
    public int SpreadCount;

    public Gun(float fireRate, float damage, float nextFire, float speed, int clipSize, int ammo, int spreadCount)
    {
        FireRate = fireRate;
        Damage = damage;
        ClipSize = clipSize;
        Speed = speed;
        Ammo = ammo;
        SpreadCount = spreadCount;
        NextFire = nextFire;
    }

    public abstract void Fire(Bullet bullet, Transform playerTransform, Transform firePointTransform);
    public abstract int ResetAmmo();

}

public class Rifle : Gun
{
    public Rifle(
    float fireRate = 0.1f,
    float damage = 5,
    float nextFire = 0.0f,
    float speed = 100f,
    int clipSize = 20,
    int ammo = 20,
    int spreadCount = 1) :
    base(fireRate, damage, nextFire, speed, clipSize, ammo, spreadCount)
    {

    }
    public override int ResetAmmo()
    {
        return Ammo = ClipSize;
    }
    public override void Fire(Bullet _bullet, Transform playerTransform, Transform firePointTransform)
    {
        for (int i = 0; i < SpreadCount; i++)
        {
            Bullet bullet = GameObject.Instantiate(_bullet, firePointTransform.position, firePointTransform.rotation) as Bullet;
            bullet.Rigidbody.velocity = playerTransform.up * Speed;
        }
    }
}

public class Shotgun : Gun
{
    float[] spreadAngle = new float[5];
    int counter = 4;
    public Shotgun(
    float fireRate = 2f,
    float damage = 15,
    float nextFire = 0.0f,
    float speed = 75,
    int clipSize = 1,
    int ammo = 1,
    int spreadCount = 5) :
    base(fireRate, damage, nextFire, speed, clipSize, ammo, spreadCount)
    {
       
        spreadAngle[0] = -40;
        spreadAngle[1] = -15;
        spreadAngle[2] = 0;
        spreadAngle[3] = 15;
        spreadAngle[4] = 30;
    }
    public override int ResetAmmo()
    {
        return Ammo = ClipSize;
    }
    public override void Fire(Bullet _bullet, Transform playerTransform, Transform firePointTransform)
    {
        for (int i = 0; i < SpreadCount; i++)
        {
            if (counter <= 0)
                counter = 4;

            var x = _bullet.transform.position.x - firePointTransform.position.x;
            var y = _bullet.transform.position.y - firePointTransform.position.y;

            float rotateAngle = (spreadAngle[counter]) + (Mathf.Atan2(y, x) * Mathf.Rad2Deg);
            Bullet bullet = GameObject.Instantiate(_bullet, firePointTransform.position, firePointTransform.rotation) as Bullet;
            bullet.Rigidbody.velocity = new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad), Mathf.Sin(rotateAngle * Mathf.Deg2Rad)).normalized * Speed;
            counter--;
        }
    }
}
public class Handgun : Gun
{
    public Handgun(
    float fireRate = 0.75f,
    float damage = 35,
    float nextFire = 0.0f,
    float speed = 100f,
    int clipSize = 6,
    int ammo = 6,
    int spreadCount = 1) :
    base(fireRate, damage, nextFire, speed, clipSize, ammo, spreadCount)
    {

    }
    public override int ResetAmmo()
    {
        return Ammo = ClipSize;
    }
    public override void Fire(Bullet _bullet, Transform playerTransform, Transform firePointTransform)
    {
        for (int i = 0; i < SpreadCount; i++)
        {
            Bullet bullet = GameObject.Instantiate(_bullet, firePointTransform.position, firePointTransform.rotation) as Bullet;
            bullet.Rigidbody.velocity = playerTransform.up * Speed;
        }
    }
}
public class Laser : Gun
{
    public Laser(
    float fireRate = 2.5f,
    float damage = 100,
    float nextFire = 0.0f,
    float speed = 200f,
    int clipSize = 1,
    int ammo = 1,
    int spreadCount = 1) :
    base(fireRate, damage, nextFire, speed, clipSize, ammo, spreadCount)
    {

    }
    public override int ResetAmmo()
    {
        return Ammo = ClipSize;
    }

    public override void Fire(Bullet _bullet, Transform playerTransform, Transform firePointTransform)
    {
        for (int i = 0; i < SpreadCount; i++)
        {
            Bullet bullet = GameObject.Instantiate(_bullet, firePointTransform.position, firePointTransform.rotation) as Bullet;
            bullet.Rigidbody.velocity = playerTransform.up * Speed;
        }
    }
}