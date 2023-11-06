using Common.Infrastructure.Factories.GameObjectsFactory;
using Common.Infrastructure.Services.Coroutines;
using Common.Infrastructure.Services.Score;
using Common.UnityLogic.Ball;
using Common.UnityLogic.Camera;
using Common.UnityLogic.StairsLogic;
using Zenject;

namespace Common.Infrastructure.Services.SceneContext
{
    public sealed class SceneContextService : ISceneContextService
    {
        public BallMovement BallMovement { get; set; }
        public Stairs Stairs { get; set; }
        public CameraFollowing Camera { get; set; }

        private IGameObjectsFactory _gameObjectsFactory;
        private IScoreService _scoreService;
        private ICoroutineRunner _coroutineRunner;

        [Inject]
        private void Construct(IGameObjectsFactory gameObjectsFactory, IScoreService scoreService, ICoroutineRunner coroutineRunner)
        {
            _gameObjectsFactory = gameObjectsFactory;
            _scoreService = scoreService;
            _coroutineRunner = coroutineRunner;
        }
        public void ResetScene()
        {
            _coroutineRunner.StopAllCoroutines();
            _scoreService.ResetScoreCalculating();
            BallMovement.ResetBall();
            Stairs.RebuildSteps();
            _gameObjectsFactory.DespawnAllObstacles();
        }
    }
}