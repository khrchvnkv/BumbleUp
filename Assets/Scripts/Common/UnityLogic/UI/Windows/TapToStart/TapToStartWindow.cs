using Common.Infrastructure.Factories.UIFactory;
using Common.Infrastructure.Services.Input;
using Common.Infrastructure.Services.SceneContext;
using Zenject;

namespace Common.UnityLogic.UI.Windows.TapToStart
{
    public sealed class TapToStartWindow : WindowBase<TapToStartWindowData>
    {
        private IInputService _inputService;
        private IUIFactory _uiFactory;
        private ISceneContextService _sceneContextService;
        
        [Inject]
        private void Construct(IInputService inputService, IUIFactory uiFactory, ISceneContextService sceneContextService)
        {
            _inputService = inputService;
            _uiFactory = uiFactory;
            _sceneContextService = sceneContextService;
        }

        protected override void PrepareForShowing()
        {
            base.PrepareForShowing();
            _inputService.ActivateInput();
            _inputService.OnTouch += HideTapToStartWindowAndStartSpawnObstacles;
        }
        protected override void PrepareForHiding()
        {
            base.PrepareForHiding();
            _inputService.OnTouch -= HideTapToStartWindowAndStartSpawnObstacles;
        }
        private void HideTapToStartWindowAndStartSpawnObstacles()
        {
            _uiFactory.Hide<TapToStartWindowData>();
            _sceneContextService.Stairs.ObstacleSpawner.Activate();
        }
    }
}