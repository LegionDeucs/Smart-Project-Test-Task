using Cysharp.Threading.Tasks;
using SLS;
using System;

namespace MyCore.StateMachine
{
    public class LoadingSceneApplicationState : ApplicationStateMachineBaseState
    {
        private LevelLoader levelLoader;
        private SaveLoadSystem sls;

        public LoadingSceneApplicationState(ApplicationContext context, SaveLoadSystem sls, LevelLoader levelLoader) : base(context)
        {
            this.sls = sls;
            this.levelLoader = levelLoader;
        }

        public override void Dispose()
        {
            
        }

        public void NextState<T>() where T:ApplicationStateMachineBaseState
        {
            if (typeof(T).Equals(typeof(MetaApplicationState)))
            {
                LoadMetaScene();
            }
            else if (typeof(T).Equals(typeof(GameApplicationState)))
            {
                LoadLevelScene();
            }
        }

        public override void OnStateEnter()
        {
            levelLoader.LoadLoadingScene();
        }

        private void LoadMetaScene()
        {
            levelLoader.LoadMetaScene(UnloadLoadingScene<MetaApplicationState>);
        }

        private void LoadLevelScene() => levelLoader.LoadLevelScene(sls.saveLoadSystemCache.GetLevelData(), UnloadLoadingScene<GameApplicationState>);

        private void UnloadLoadingScene<T>() where T : ApplicationStateMachineBaseState
            => levelLoader.UnloadLoadingScene(()=>Context.StateMachine.EnterState<T>());


        public override void OnStateExit()
        {
            
        }
    }
}