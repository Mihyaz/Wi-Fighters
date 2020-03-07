using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    string Shoot();
    string StopShooting();
    string Reload();
    string StopReload();
    bool isShooting { get; set; }
    bool isReloading { get; set; }
}
