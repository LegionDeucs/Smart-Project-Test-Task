using MyCore.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelCoreInstaller : MonoInstaller
{
    [SerializeField] private CameraController cameraController;

    public override void InstallBindings()
    {
        Container.Bind<CameraController>().FromInstance(cameraController).AsSingle().NonLazy();
    }
}
