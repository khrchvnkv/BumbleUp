using System.Collections.Generic;
using Common.Infrastructure.Factories.Zenject;
using Common.Infrastructure.Services.AssetsManagement;
using Common.UnityLogic.Ball;
using Common.UnityLogic.StairsLogic;
using NTC.Pool;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure.Factories.GameObjectsFactory
{
    public sealed class GameObjectsFactory : IGameObjectsFactory
    {
        private const string BallPath = "Gameplay/Ball";
        private const string ObstaclePath = "Gameplay/Obstacle";
        
        private readonly IAssetProvider _assetProvider;
        private readonly IZenjectFactory _zenjectFactory;
        private readonly DiContainer _diContainer;

        private readonly Dictionary<string, GameObject> _loadedPrefabs = new();
        private readonly List<ObstacleMovement> _createdObstacles = new();

        public GameObjectsFactory(IAssetProvider assetProvider, IZenjectFactory zenjectFactory, DiContainer diContainer)
        {
            _assetProvider = assetProvider;
            _zenjectFactory = zenjectFactory;
            _diContainer = diContainer;
        }
        public BallMovement CreateBall()
        {
            var prefab = GetPrefab(BallPath);
            var instance = _zenjectFactory.Instantiate(prefab);
            var ballMovement = instance.GetComponent<BallMovement>();
            
            return ballMovement;
        }
        public ObstacleMovement SpawnObstacle(Vector3 position)
        {
            var prefab = GetPrefab(ObstaclePath);
            var instance = NightPool.Spawn(prefab, position, Quaternion.identity);
            var obstacle = instance.GetComponent<ObstacleMovement>();
            _createdObstacles.Add(obstacle);
            _diContainer.Inject(obstacle);
            
            return obstacle;
        }
        public void DespawnObstacle(ObstacleMovement obstacleMovement)
        {
            NightPool.Despawn(obstacleMovement);
            _createdObstacles.Remove(obstacleMovement);
        }
        public void DespawnAllObstacles()
        {
            foreach (var obstacle in _createdObstacles)
            {
                NightPool.Despawn(obstacle);
            }
            _createdObstacles.Clear();
        }
        private GameObject GetPrefab(in string prefabPath)
        {
            if (_loadedPrefabs.TryGetValue(prefabPath, out var prefab)) return prefab;

            prefab = _assetProvider.Load(prefabPath);
            _loadedPrefabs[prefabPath] = prefab;
            return prefab;
        }
    }
}