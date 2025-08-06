using MyCore.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private PlayerProvider playerProvider;
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] private GameplayContext gameplayContext;

    [SerializeField] private WinUI winUI;
    [SerializeField] private LevelUI levelUI;
    [SerializeField] private ScoreSystem scoreSystem;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameplayContext>().FromInstance(gameplayContext).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameplayStateMachine>().AsSingle().NonLazy();

        Container.Bind<PlayerProvider>().FromInstance(playerProvider).AsSingle().NonLazy();
        Container.Bind<LevelGenerator>().FromInstance(levelGenerator).AsSingle().NonLazy();


        Container.Bind<WinUI>().FromInstance(winUI).AsSingle().NonLazy();
        Container.Bind<LevelUI>().FromInstance(levelUI).AsSingle().NonLazy();

        Container.Bind<ScoreSystem>().FromInstance(scoreSystem).AsSingle().NonLazy();
    }
}
