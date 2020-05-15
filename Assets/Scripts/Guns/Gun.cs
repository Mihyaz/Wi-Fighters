using Boo.Lang;
using DG.Tweening;
using System.IO.IsolatedStorage;
using UnityEngine;
public enum GunClasses
{
    Rifle,
    Shotgun,
    Handgun,
    Laser
}

public abstract class Gun
{
    public IShot ShootingType;

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

    public virtual void Fire(Bullet bullet, Transform firePointTransform)
    {
        ShootingType.Fire(bullet, firePointTransform);
    }

    public void SetShootingAbility(IShot newShootingType)
    {
        this.ShootingType = newShootingType;
    }

    public virtual int ResetAmmo()
    {
        return Ammo = ClipSize;
    }

}

public interface IShot
{
    void Fire(Bullet _bullet, Transform firePointTransform);
}

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

public class SpreadShot : IShot
{
    private int _angle = -30;
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
            Transform points = firePointTransform;
            points.eulerAngles = new Vector3(0, 0, _angle + (i * 15));

            Bullet bullet = GameObject.Instantiate(_bullet, firePointTransform.position, firePointTransform.rotation) as Bullet;
            bullet.transform.Rotate(bullet.transform.rotation.x, bullet.transform.rotation.y, points.eulerAngles.z);
            bullet.Rigidbody.velocity = points.up * _speed;
        }
    }
}