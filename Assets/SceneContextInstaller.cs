using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace SpaceDemo
{
    public class SceneContextInstaller : MonoInstaller
    {
        public GameObject PlayerShipPrefab;
        public GameObject MarkerPrefab;
        public override void InstallBindings()
        {
            Container.Bind<PlayerController>().FromComponentInNewPrefab(PlayerShipPrefab).AsSingle();
            Container.Bind<Marker>().FromComponentInNewPrefab(MarkerPrefab).AsSingle();

            Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GUIManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<TimeManager>().FromComponentInHierarchy().AsSingle();
        }
}
}