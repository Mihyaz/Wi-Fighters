﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour, IAttack
{
    public bool IsShooting { get ; set ; }
    public bool IsReloading { get ; set ; }

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
