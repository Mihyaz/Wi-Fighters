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
        ShootingType = new LinearShot(spreadCount, speed);
    }
}
