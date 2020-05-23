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
        ShootingType = new LinearShot(this);
    }
}
