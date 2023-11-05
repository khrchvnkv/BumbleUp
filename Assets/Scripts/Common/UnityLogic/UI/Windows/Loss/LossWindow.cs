using System.Collections;
using Common.Infrastructure.Factories.UIFactory;
using Common.Infrastructure.Services.Coroutines;
using Common.Infrastructure.Services.SceneContext;
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

        [Inject]
        private void Construct(ISceneContextService sceneContextService, IUIFactory uiFactory, ICoroutineRunner coroutineRunner)
        {
            _sceneContextService = sceneContextService;
            _uiFactory = uiFactory;
            _coroutineRunner = coroutineRunner;
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
            _coroutineRunner.ExecuteInNextFrame(() => _uiFactory.Show(new TapToStartWindowData()));
        }
    }
}