using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour, IAttack
{
    public bool isShooting { get ; set ; }
    public bool isReloading { get ; set ; }

    public string Shoot()
    {
        throw new System.NotImplementedException();
    }

    public string Reload()
    {
        throw new System.NotImplementedException();
    }

    public string StopShooting()
    {
        throw new System.NotImplementedException();
    }

    public string StopReload()
    {
        throw new System.NotImplementedException();
    }
}
