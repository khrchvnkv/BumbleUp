using Common.Infrastructure.Factories.UIFactory;
using Common.Infrastructure.Services.Input;
using Common.Infrastructure.Services.Progress;
using Common.Infrastructure.Services.SaveLoad;
using Common.Infrastructure.Services.SceneContext;
using Common.Infrastructure.Services.Score;
using Common.UnityLogic.UI.Windows.Loss;
using Common.UnityLogic.UI.Windows.TapToStart;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Ball
{
    [RequireComponent(typeof(BallMovement))]
    [RequireComponent(typeof(BallCollision))]
    public sealed class BallComponent : MonoBehaviour
    {
        [SerializeField] private BallMovement _movement;
        [SerializeField] private BallCollision _collision;

        private IUIFactory _uiFactory;
        private IInputService _inputService;
        private ISceneContextService _sceneContextService;
        private IPersistentProgressService _persistentProgressService;
        private ISaveLoadService _saveLoadService;
        private IScoreService _scoreService;

        [Inject]
        private void Construct(IUIFactory uiFactory, IInputService inputService, ISceneContextService sceneContextService, 
            IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService, IScoreService scoreService)
        {
            _uiFactory = uiFactory;
            _inputService = inputService;
            _sceneContextService = sceneContextService;
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;
            _scoreService = scoreService;
        }
        public void ResetBall()
        {
            _movement.ResetMovement();
            _collision.ResetCollision();
        }
        private void OnValidate()
        {
            _movement ??= gameObject.GetComponent<BallMovement>();
            _collision ??= gameObject.GetComponent<BallCollision>();
        }
        private void OnEnable()
        {
            _movement.BallFallingStarted += OnFailAction;
            _movement.BallFallingCompleted += AfterFailAction;

            _collision.BallCollisionStarted += OnFailAction;
            _collision.BallCollisionCompleted += AfterFailAction;
        }
        private void OnDisable()
        {
            _movement.BallFallingStarted -= OnFailAction;
            _movement.BallFallingCompleted -= AfterFailAction;
            
            _collision.BallCollisionStarted -= OnFailAction;
            _collision.BallCollisionCompleted -= AfterFailAction;
        }
        private void OnFailAction()
        {
            _uiFactory.Hide<TapToStartWindowData>();
            _inputService.DeactivateInput();
            _sceneContextService.Stairs.ObstacleSpawner.Deactivate();
        }
        private void AfterFailAction()
        {
            var currentScore = _scoreService.Score;
            var bestScore = _persistentProgressService.SaveData.Progress.BestResult;

            if (currentScore > bestScore)
            {
                _persistentProgressService.SaveData.Progress.BestResult = currentScore;
                _saveLoadService.SaveData();
            }
            
            _uiFactory.Show(new LossWindowData(currentScore, bestScore));
        }
    }
}