using UnityEngine;
using MyCore.StateMachine;
using Zenject;
using SLS;

namespace Installers
{
    public class ApplicationInstaller : MonoInstaller
    {
        [SerializeField] private LevelLoader levelLoader;

        [SerializeField] private InputSystemProcessorContext inputSystemProcessorContext;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ApplicationContext>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ApplicationStateMachine>().AsSingle().NonLazy();

            InstallSaveSystem();

            Container.Bind<LevelLoader>().FromInstance(levelLoader).AsSingle().NonLazy();

            InstallInputSystem();

            InstallServices();
        }

        private void InstallSaveSystem()
        {
            Container.Bind<SaveLoadSystem>().AsSingle();
        }

        private void InstallServices()
        {
            
        }

        private void InstallInputSystem()
        {
            Container.Bind<InputSystemProcessorContext>().FromInstance(inputSystemProcessorContext).AsSingle().NonLazy();
            Container.Bind<InputSystemProcessor>().AsSingle().NonLazy();
        }
    }
}