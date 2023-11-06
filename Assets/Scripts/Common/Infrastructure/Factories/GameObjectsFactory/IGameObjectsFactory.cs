using Common.UnityLogic.Ball;
using Common.UnityLogic.StairsLogic;
using UnityEngine;

namespace Common.Infrastructure.Factories.GameObjectsFactory
{
    public interface IGameObjectsFactory
    {
        BallMovement CreateBall();
        ObstacleMovement SpawnObstacle(Vector3 position);
        void DespawnObstacle(ObstacleMovement obstacleMovement);
        void DespawnAllObstacles();
    }
}