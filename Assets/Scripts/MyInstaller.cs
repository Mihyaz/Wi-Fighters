using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MyInstaller : MonoInstaller
{
    [SerializeField] private GameUI _ui;
    [SerializeField] private MyIPAddress _myIPAddress;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private SpawnPointHandler _spawnPointHandler;

    public override void InstallBindings()
    {
        Container.Bind<GameUI>().FromInstance(_ui);
        Container.Bind<MyIPAddress>().FromInstance(_myIPAddress);
        Container.Bind<Bullet>().FromInstance(_bullet);
        Container.Bind<SpawnPointHandler>().FromInstance(_spawnPointHandler);
    }
}
