using Common.Infrastructure.Factories.GameObjectsFactory;
using Common.Infrastructure.Services.SceneContext;
using Common.UnityLogic.Camera;
using Common.UnityLogic.StairsLogic;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.SceneContext
{
    public sealed class SceneContextComponent : MonoBehaviour
    {
        [SerializeField] private CameraFollowing _camera;
        [SerializeField] private Stairs _stairs;

        private IGameObjectsFactory _gameObjectsFactory;
        private ISceneContextService _sceneContextService;

        [Inject]
        private void Construct(IGameObjectsFactory gameObjectsFactory, ISceneContextService sceneContextService)
        {
            _gameObjectsFactory = gameObjectsFactory;
            _sceneContextService = sceneContextService;

            InitSceneContext();
        }
        private void InitSceneContext()
        {
            _sceneContextService.BallMovement = _gameObjectsFactory.CreateBall();
            _sceneContextService.Camera = _camera;
            _sceneContextService.Stairs = _stairs;
            
            _sceneContextService.ResetScene();
        }
    }
}