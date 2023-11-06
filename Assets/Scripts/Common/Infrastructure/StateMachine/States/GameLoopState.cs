using Common.Infrastructure.Factories.UIFactory;
using Common.Infrastructure.Services.Score;
using Common.UnityLogic.UI.Windows.GameHud;
using Common.UnityLogic.UI.Windows.TapToStart;

namespace Common.Infrastructure.StateMachine.States
{
    public class GameLoopState : State, IState
    {
        private readonly IUIFactory _uiFactory;
        private readonly IScoreService _scoreService;

        public GameLoopState(IUIFactory uiFactory, IScoreService scoreService)
        {
            _uiFactory = uiFactory;
            _scoreService = scoreService;
        }
        public void Enter()
        {
            _uiFactory.Show(new TapToStartWindowData());
            _uiFactory.Show(new GameHudWindowData(_scoreService));
        }
        public override void Exit() { }
    }
}