using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem : MonoBehaviour, IAttack
{
    public bool isShooting { get; set; }
    public bool isReloading { get; set; }

    public string Shoot()
    {
        return "Shoot";
    }

    public string StopShooting()
    {
        return "Stop";
    }

    public string Reload()
    {
        return "Reload";
    }

    public string StopReload()
    {
        return "Stop";
    }
}
