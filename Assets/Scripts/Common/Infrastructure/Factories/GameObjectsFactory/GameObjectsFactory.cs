using Common.Infrastructure.Factories.Zenject;
using Common.Infrastructure.Services.AssetsManagement;
using Common.UnityLogic.Ball;

namespace Common.Infrastructure.Factories.GameObjectsFactory
{
    public sealed class GameObjectsFactory : IGameObjectsFactory
    {
        private const string BallPath = "Gameplay/Ball";
        
        private readonly IAssetProvider _assetProvider;
        private readonly IZenjectFactory _zenjectFactory;

        public GameObjectsFactory(IAssetProvider assetProvider, IZenjectFactory zenjectFactory)
        {
            _assetProvider = assetProvider;
            _zenjectFactory = zenjectFactory;
        }

        public BallMovement CreateBall()
        {
            var prefab = _assetProvider.Load(BallPath);
            var instance = _zenjectFactory.Instantiate(prefab);
            var ballMovement = instance.GetComponent<BallMovement>();
            
            return ballMovement;
        }
    }
}