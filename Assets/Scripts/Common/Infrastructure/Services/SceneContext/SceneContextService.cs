using Common.UnityLogic.Ball;
using Common.UnityLogic.Camera;
using Common.UnityLogic.StairsLogic;

namespace Common.Infrastructure.Services.SceneContext
{
    public sealed class SceneContextService : ISceneContextService
    {
        public BallMovement BallMovement { get; set; }
        public Stairs Stairs { get; set; }
        public CameraFollowing Camera { get; set; }
        
        public void ResetScene()
        {
            BallMovement.ResetBall();
            Stairs.RebuildSteps();
        }
    }
}