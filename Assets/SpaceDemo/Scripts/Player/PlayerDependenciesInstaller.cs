using UnityEngine;
using Zenject;

namespace SpaceDemo
{
    public class PlayerDependenciesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Storage>().FromComponentsOnRoot().AsSingle();
            Container.Bind<Wallet>().FromComponentsOnRoot().AsSingle();
            Container.Bind<PlayerMovement>().FromComponentsOnRoot().AsSingle();
        }
    }
}