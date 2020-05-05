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

    public virtual void Fire(Bullet bullet, Transform playerTransform, Transform firePointTransform)
    {
        ShootingType.Fire(bullet, playerTransform, firePointTransform);
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
    void Fire(Bullet _bullet, Transform playerTransform, Transform firePointTransform);
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

    public void Fire(Bullet _bullet, Transform playerTransform, Transform firePointTransform)
    {
        for (int i = 0; i < _spreadCount; i++)
        {
            Bullet bullet = GameObject.Instantiate(_bullet, firePointTransform.position, firePointTransform.rotation) as Bullet;
            bullet.Rigidbody.velocity = playerTransform.up * _speed;
        }
    }
}

public class SpreadShot : IShot
{
    private float[] _spreadAngle = new float[5];
    private int _counter = 4;

    private int _spreadCount;
    private float _speed;

    public SpreadShot(int SpreadCount, float Speed)
    {
        _spreadCount = SpreadCount;
        _speed = Speed;

        _spreadAngle[0] = -30;
        _spreadAngle[1] = -15;
        _spreadAngle[2] = 0;
        _spreadAngle[3] = 15;
        _spreadAngle[4] = 30;

    }
    public void Fire(Bullet _bullet, Transform playerTransform, Transform firePointTransform)
    {
        for (int i = 0; i < _spreadCount; i++)
        {
            if (_counter <= 0)
                _counter = 4;

            var x = _bullet.transform.position.x - firePointTransform.position.x;
            var y = _bullet.transform.position.y - firePointTransform.position.y;

            float rotateAngle = (_spreadAngle[_counter]) + (Mathf.Atan2(y, x) * Mathf.Rad2Deg);
            Bullet bullet = GameObject.Instantiate(_bullet, firePointTransform.position, firePointTransform.rotation) as Bullet;
            bullet.Rigidbody.velocity = new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad), Mathf.Sin(rotateAngle * Mathf.Deg2Rad)).normalized * _speed;
            _counter--;
        }
    }
}