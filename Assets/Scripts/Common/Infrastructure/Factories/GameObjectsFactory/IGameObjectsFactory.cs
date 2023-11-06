using Common.UnityLogic.Ball;
using Common.UnityLogic.StairsLogic;
using UnityEngine;

namespace Common.Infrastructure.Factories.GameObjectsFactory
{
    public interface IGameObjectsFactory
    {
        BallComponent CreateBall();
        ObstacleMovement SpawnObstacle(Vector3 position);
        void DespawnObstacle(ObstacleMovement obstacleMovement);
        void DespawnAllObstacles();
    }
}