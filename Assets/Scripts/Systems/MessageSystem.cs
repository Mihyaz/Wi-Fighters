using UnityEngine;

public class MessageSystem : MonoBehaviour, IAttack
{
    public bool IsShooting { get; set; }
    public bool IsReloading { get; set; }

    public string Shoot() => "Shoot";

    public string StopShooting() => "Stop";

    public string Reload() => "Reload";

    public string StopReload() => "Stop";
}
