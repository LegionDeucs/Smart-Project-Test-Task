using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MetaSceneInstaller : MonoInstaller
{
    [SerializeField] private MainMenuUI mainMenuUI;

    public override void InstallBindings()
    {
        Container.Bind<MainMenuUI>().FromInstance(mainMenuUI).AsSingle().NonLazy();
    }
}
