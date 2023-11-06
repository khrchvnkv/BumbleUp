using System;
using System.Collections;
using Common.Infrastructure.Factories.GameObjectsFactory;
using Common.Infrastructure.Services.Coroutines;
using Common.Infrastructure.Services.SceneContext;
using Common.Infrastructure.Services.StaticData;
using Common.StaticData;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.StairsLogic
{
    public sealed class ObstacleSpawner : MonoBehaviour
    {
        private ObstacleStaticData _obstacleStaticData;
        private IGameObjectsFactory _gameObjectsFactory;
        private ICoroutineRunner _coroutineRunner;
        private ISceneContextService _sceneContextService;

        private Coroutine _coroutine;
        
        [Inject]
        private void Construct(IStaticDataService staticDataService, IGameObjectsFactory gameObjectsFactory, 
            ICoroutineRunner coroutineRunner, ISceneContextService sceneContextService)
        {
            _obstacleStaticData = staticDataService.GameStaticData.ObstacleStaticData;
            _gameObjectsFactory = gameObjectsFactory;
            _coroutineRunner = coroutineRunner;
            _sceneContextService = sceneContextService;
        }
        public void Activate()
        {
            _coroutineRunner.StopCoroutineSafe(_coroutine);
            _coroutine = _coroutineRunner.StartCoroutine(SpawnCoroutine());
        }
        public void Deactivate() => _coroutineRunner.StopCoroutineSafe(_coroutine);
        private IEnumerator SpawnCoroutine()
        {
            var delay = new WaitForSeconds(_obstacleStaticData.TimeBtwSpawn);
            while (true)
            {
                var path = _sceneContextService.Stairs.GetRandomObstaclePathPoints();
                if (path.Count == 0) throw new Exception("The point array must not be empty");
                
                var spawnPoint = path[0];
                path.RemoveAt(0);
                var obstacle = _gameObjectsFactory.SpawnObstacle(spawnPoint);
                obstacle.SetPath(path);
                yield return delay;
            }
        }
    }
}