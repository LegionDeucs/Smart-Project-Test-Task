using Cysharp.Threading.Tasks;
using SLS;

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

        public override void OnStateEnter()
        {
            levelLoader.LoadLoadingScene(LoadLevelScene);
        }

        private void LoadLevelScene() => levelLoader.LoadLevelScene(sls.saveLoadSystemCache.GetLevelData(), UnloadLoadingScene);

        private void UnloadLoadingScene() => levelLoader.UnloadLoadingScene(EnterGameState);

        private void EnterGameState() => Context.StateMachine.EnterState<GameApplicationState>();

        public override void OnStateExit()
        {
            
        }
    }
}