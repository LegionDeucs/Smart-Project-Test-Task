namespace MyCore.StateMachine
{
    public class GameState : GameplayStateMachineBaseState
    {
        private readonly WinUI winUI;
        private readonly LevelUI levelUI;
        private readonly PlayerProvider playerProvider;
        private readonly CameraController cameraController;

        public GameState(GameplayContext context, WinUI winUI, LevelUI levelUI, PlayerProvider playerProvider, CameraController cameraController) : base(context)
        {
            this.winUI = winUI;
            this.levelUI = levelUI;
            this.playerProvider = playerProvider;
            this.cameraController = cameraController;
        }

        public override void Dispose()
        {
            
        }

        public override void OnStateEnter()
        {
            winUI.ForceHide();
            playerProvider.PlayerController.OnStep += PlayerController_OnStep;
            cameraController.SetupOffset(Context.GetMazeSize().magnitude);
            //Create InputProvider. Player provider sub at InputProcessor here to better control game state

        }

        private void PlayerController_OnStep()
        {
            levelUI.UpdateStepsCounter(playerProvider.PlayerController.GetStepsCount());
        }

        public override void OnStateExit()
        {
            
        }
    }
}