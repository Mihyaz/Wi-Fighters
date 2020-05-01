public interface IAttack
{
    string Shoot();
    string StopShooting();
    string Reload();
    string StopReload();
    bool _isShooting { get; set; }
    bool _isReloading { get; set; }
}
