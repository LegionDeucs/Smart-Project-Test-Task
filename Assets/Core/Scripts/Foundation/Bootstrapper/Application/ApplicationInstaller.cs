using UnityEngine;
using MyCore.StateMachine;
using Zenject;
using SLS;

namespace Installers
{
    public class ApplicationInstaller : MonoInstaller
    {
        [SerializeField] private LevelLoader levelLoader;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ApplicationContext>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ApplicationStateMachine>().AsSingle().NonLazy();

            InstallSaveSystem();

            Container.Bind<LevelLoader>().FromInstance(levelLoader).AsSingle().NonLazy();

            InstallService();
        }

        private void InstallSaveSystem()
        {
            Container.Bind<SaveLoadSystem>().AsSingle();
        }

        private void InstallService()
        {
        }
    }
}