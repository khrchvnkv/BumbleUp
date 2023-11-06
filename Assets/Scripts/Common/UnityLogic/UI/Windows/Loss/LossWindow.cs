using Common.Infrastructure.Factories.UIFactory;
using Common.Infrastructure.Services.Coroutines;
using Common.Infrastructure.Services.SceneContext;
using Common.UnityLogic.UI.Windows.GameHud;
using Common.UnityLogic.UI.Windows.TapToStart;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common.UnityLogic.UI.Windows.Loss
{
    public sealed class LossWindow : WindowBase<LossWindowData>
    {
        private const string BestResultFormat = "Best result: {0}";
        
        [SerializeField] private Button _restartButton;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _bestScoreText;
        [SerializeField] private GameObject _newBstScoreText;

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
            UpdateScoreView();
            _sceneContextService.ResetScene();
        }
        protected override void PrepareForHiding()
        {
            base.PrepareForHiding();
            _restartButton.onClick.RemoveListener(RestartGame);
        }
        private void UpdateScoreView()
        {

            _scoreText.text = WindowData.CurrentResult.ToString();
            if (WindowData.CurrentResult > WindowData.BestResult)
            {
                _newBstScoreText.SetActive(true);
                _bestScoreText.gameObject.SetActive(false);
            }
            else
            {
                _newBstScoreText.SetActive(false);
                _bestScoreText.gameObject.SetActive(true);
                _bestScoreText.text = string.Format(BestResultFormat, WindowData.BestResult);
            }
        }
        private void RestartGame()
        {            
            _uiFactory.Hide<LossWindowData>();
            _coroutineRunner.ExecuteInNextFrame(() =>
            {
                _uiFactory.Show(new TapToStartWindowData());
                _uiFactory.Show(new GameHudWindowData());
            });
        }
    }
}