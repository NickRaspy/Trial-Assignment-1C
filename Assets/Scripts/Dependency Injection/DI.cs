using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DI : MonoInstaller<DI>
{
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromComponentOn(gameObject).AsSingle();
    }
}
