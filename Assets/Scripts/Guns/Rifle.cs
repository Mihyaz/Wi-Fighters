public class Rifle : Gun
{
    public Rifle(
    float fireRate = 0.1f,
    float damage = 15,
    float nextFire = 0.0f,
    float speed = 100f,
    int clipSize = 20,
    int ammo = 20,
    int spreadCount = 1) :
    base(fireRate, damage, nextFire, speed, clipSize, ammo, spreadCount)
    {
        ShootingType = new LinearShot(spreadCount, speed);
    }
}
