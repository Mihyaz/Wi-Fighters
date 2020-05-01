//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using OnurMihyaz;

//public class User : MonoBehaviour
//{
//    //public Client Client;
//    IMove _movement;
//    IAttack _attack;

//    private void Start()
//    {
//        _movement = GetComponent<IMove>();
//        _attack = GetComponent<IAttack>();
//    }

//    private void Update()
//    {
//        Client.SendMessageToServer(
//            _movement.Move().ToString() + "#" + _movement.Rotate().ToString() + "#" + Shooting() + "#" + Reloading()
//            + "#" + GetName(), "Move");
//    }

//    private string Shooting()
//    {
//        if (_attack.isShooting)
//        {
//            return _attack.Shoot();
//        }
//        else
//        {
//            return _attack.StopShooting();
//        }
//    }

//    private string Reloading()
//    {
//        if (_attack.isReloading)
//        {
//            return _attack.Reload();
//        }
//        else
//        {
//            return _attack.StopReload();
//        }
//    }

//    private string GetName()
//    {
//        return Client.MyName;
//    }

//    public void StartShooting()
//    {
//        _attack.isShooting = true;
//    }

//    public void StopShooting()
//    {
//        _attack.isShooting = false;
//    }

//    public void StartReloading()
//    {
//        _attack.isReloading = true;
//    }

//    public void StopReloading()
//    {
//        _attack.isReloading = false;
//    }

//}
