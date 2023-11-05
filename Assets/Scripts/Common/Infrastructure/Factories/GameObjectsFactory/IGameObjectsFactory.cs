using Common.UnityLogic.Ball;

namespace Common.Infrastructure.Factories.GameObjectsFactory
{
    public interface IGameObjectsFactory
    {
        BallMovement CreateBall();
    }
}