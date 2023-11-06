using System.Collections;
using Common.Infrastructure.Factories.UIFactory;
using Common.Infrastructure.Services.Coroutines;
using Common.Infrastructure.Services.SceneContext;
using Common.Infrastructure.Services.Score;
using Common.UnityLogic.UI.Windows.GameHud;
using Common.UnityLogic.UI.Windows.TapToStart;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common.UnityLogic.UI.Windows.Loss
{
    public sealed class LossWindow : WindowBase<LossWindowData>
    {
        [SerializeField] private Button _restartButton;

        private ISceneContextService _sceneContextService;
        private IUIFactory _uiFactory;
        private ICoroutineRunner _coroutineRunner;
        private IScoreService _scoreService;

        [Inject]
        private void Construct(ISceneContextService sceneContextService, IUIFactory uiFactory, 
            ICoroutineRunner coroutineRunner, IScoreService scoreService)
        {
            _sceneContextService = sceneContextService;
            _uiFactory = uiFactory;
            _coroutineRunner = coroutineRunner;
            _scoreService = scoreService;
        }
        protected override void PrepareForShowing()
        {
            base.PrepareForShowing();
            _restartButton.onClick.AddListener(RestartGame);
            _sceneContextService.ResetScene();
        }
        protected override void PrepareForHiding()
        {
            base.PrepareForHiding();
            _restartButton.onClick.RemoveListener(RestartGame);
        }
        private void RestartGame()
        {            
            _uiFactory.Hide<LossWindowData>();
            _coroutineRunner.ExecuteInNextFrame(() =>
            {
                _uiFactory.Show(new TapToStartWindowData());
                _uiFactory.Show(new GameHudWindowData(_scoreService));
            });
        }
    }
}