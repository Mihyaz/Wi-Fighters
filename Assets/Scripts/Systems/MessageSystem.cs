using UnityEngine;

public class MessageSystem : MonoBehaviour, IAttack
{
    public bool _isShooting { get; set; }
    public bool _isReloading { get; set; }

    public string Shoot() => "Shoot";

    public string StopShooting() => "Stop";

    public string Reload() => "Reload";

    public string StopReload() => "Stop";
}
