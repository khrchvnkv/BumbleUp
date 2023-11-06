using Common.Infrastructure.Factories.UIFactory;
using Common.UnityLogic.UI.Windows.GameHud;
using Common.UnityLogic.UI.Windows.TapToStart;

namespace Common.Infrastructure.StateMachine.States
{
    public class GameLoopState : State, IState
    {
        private readonly IUIFactory _uiFactory;

        public GameLoopState(IUIFactory uiFactory) => _uiFactory = uiFactory;
        public void Enter()
        {
            _uiFactory.Show(new TapToStartWindowData());
            _uiFactory.Show(new GameHudWindowData());
        }
        public override void Exit() { }
    }
}