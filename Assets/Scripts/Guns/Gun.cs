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

    public void SetShootingAbility(IShot newShootingType)
    {
        this.ShootingType = newShootingType;
    }

    public virtual void Fire(Bullet bullet, Transform firePointTransform)
    {
        ShootingType.Fire(bullet, firePointTransform);
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
