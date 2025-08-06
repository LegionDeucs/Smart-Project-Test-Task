using SLS;

namespace MyCore.StateMachine
{
    public class WinGameState : GameplayStateMachineBaseState
    {
        private readonly PlayerProvider playerProvider;
        private readonly SaveLoadSystem saveLoadSystem;
        private readonly ScoreSystem scoreSystem;
        private readonly WinUI winUI;
        private readonly ApplicationContext applicationContext;

        public WinGameState(GameplayContext context, PlayerProvider playerProvider, 
            SaveLoadSystem saveLoadSystem, ScoreSystem scoreSystem, WinUI winUI, ApplicationContext applicationContext) : base(context)
        {
            this.playerProvider = playerProvider;
            this.saveLoadSystem = saveLoadSystem;
            this.scoreSystem = scoreSystem;
            this.winUI = winUI;
            this.applicationContext = applicationContext;
        }

        public override void Dispose()
        {

        }

        public override void OnStateEnter()
        {
            playerProvider.PlayerController.DisableMovement();
            int score = scoreSystem.CalculateScore(Context.GetMazeSize(), Context.GetGameplayDuration(), playerProvider.PlayerController.GetStepsCount());
            bool isMaxScore = score > saveLoadSystem.saveLoadSystemCache.scoreSaveData.MaxScore;

            if (isMaxScore)
            {
                saveLoadSystem.saveLoadSystemCache.scoreSaveData.MaxScore = score;
                saveLoadSystem.Save();
            }

            winUI.Show((int)Context.GetGameplayDuration().TotalSeconds, playerProvider.PlayerController.GetStepsCount(), score, isMaxScore);
            winUI.OnMainMenuButtonClicked += WinUI_OnMainMenuButtonClicked;
            winUI.OnNewGameButtonClicked += WinUI_OnNewGameButtonClicked;
        }

        private void WinUI_OnNewGameButtonClicked()
        {
            winUI.OnMainMenuButtonClicked -= WinUI_OnMainMenuButtonClicked;
            winUI.OnNewGameButtonClicked -= WinUI_OnNewGameButtonClicked;

            applicationContext.StateMachine.EnterState<LoadingSceneApplicationState>().NextState<GameApplicationState>();
        }

        private void WinUI_OnMainMenuButtonClicked()
        {
            winUI.OnMainMenuButtonClicked -= WinUI_OnMainMenuButtonClicked;
            winUI.OnNewGameButtonClicked -= WinUI_OnNewGameButtonClicked;

            applicationContext.StateMachine.EnterState<LoadingSceneApplicationState>().NextState<MetaApplicationState>();
        }

        public override void OnStateExit()
        {

        }
    }
}