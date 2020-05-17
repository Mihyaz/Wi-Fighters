public interface IAttack
{
    string Shoot();
    string StopShooting();
    string Reload();
    string StopReload();
    bool IsShooting { get; set; }
    bool IsReloading { get; set; }
}
