using Common.UnityLogic.Ball;
using Common.UnityLogic.Camera;
using Common.UnityLogic.StairsLogic;

namespace Common.Infrastructure.Services.SceneContext
{
    public interface ISceneContextService
    {
        public BallComponent Ball { get; set; }
        public Stairs Stairs { get; set; }
        public CameraFollowing Camera { get; set; }

        void ResetScene();
    }
}