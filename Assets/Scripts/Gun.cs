using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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

    public abstract Vector2 Fire(Bullet bullet, Transform playerTransform);
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
    public override Vector2 Fire(Bullet bullet, Transform playerTransform)
    {
        return playerTransform.up * Speed;
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
    public override Vector2 Fire(Bullet bullet, Transform playerTransform)
    {
        return playerTransform.up * Speed;
    }
}
public class Shotgun : Gun
{
    float[] spreadAngle = new float[5];
    int i = 4;
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
       
        spreadAngle[0] = 45;
        spreadAngle[1] = -15;
        spreadAngle[2] = 0;
        spreadAngle[3] = 15;
        spreadAngle[4] = 30;
    }
    public override int ResetAmmo()
    {
        return Ammo = ClipSize;
    }
    public override Vector2 Fire(Bullet bullet, Transform playerTransform)
    {
        if (i < 0)
            i = 4;

        var x = bullet.transform.position.x - playerTransform.position.x;
        var y = bullet.transform.position.y - playerTransform.position.y;

        float rotateAngle = (spreadAngle[i]) + (Mathf.Atan2(y, x) * Mathf.Rad2Deg);
        i--;
        return new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad), Mathf.Sin(rotateAngle * Mathf.Deg2Rad)).normalized * Speed;
    }
}