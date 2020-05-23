public class Shotgun : Gun
{
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
        ShootingType = new SpreadShot(this);
    }
}
