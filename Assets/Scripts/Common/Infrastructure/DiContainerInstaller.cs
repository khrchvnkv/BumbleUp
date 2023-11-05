using Common.Infrastructure.Factories.GameObjectsFactory;
using Common.Infrastructure.Factories.UIFactory;
using Common.Infrastructure.Factories.Zenject;
using Common.Infrastructure.Services.AssetsManagement;
using Common.Infrastructure.Services.Coroutines;
using Common.Infrastructure.Services.DontDestroyOnLoadCreator;
using Common.Infrastructure.Services.Input;
using Common.Infrastructure.Services.MonoUpdate;
using Common.Infrastructure.Services.Progress;
using Common.Infrastructure.Services.SaveLoad;
using Common.Infrastructure.Services.SceneContext;
using Common.Infrastructure.Services.SceneLoading;
using Common.Infrastructure.Services.StaticData;
using Common.Infrastructure.StateMachine;
using Common.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure
{
    public sealed class DiContainerInstaller : MonoInstaller
    {
        [SerializeField] private UpdateService _updateService;
        [SerializeField] private DontDestroyOnLoadCreator _dontDestroyOnLoadCreator;
        [SerializeField] private CoroutineRunner _coroutineRunner;

        public override void InstallBindings()
        {
            BindGameStateMachine();
            BindServices();
            BindMonoServices();
            BindFactories();
        }
        private void BindMonoServices()
        {
            Container.Bind<IUpdateService>().FromInstance(_updateService).AsSingle();
            Container.Bind<IDontDestroyOnLoadCreator>().FromInstance(_dontDestroyOnLoadCreator).AsSingle();
            Container.Bind<ICoroutineRunner>().FromInstance(_coroutineRunner).AsSingle();
        }
        private void BindServices()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().FromNew().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().FromNew().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().FromNew().AsSingle();
            Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().FromNew().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().FromNew().AsSingle();
            Container.Bind<ISceneContextService>().To<SceneContextService>().FromNew().AsSingle();
            
#if UNITY_EDITOR
            Container.Bind<IInputService>().To<StandaloneInputService>().FromNew().AsSingle();
#else
            Container.Bind<IInputService>().To<MobileInputService>().FromNew().AsSingle();
#endif
        }
        private void BindGameStateMachine()
        {
            Container.Bind<GameStateMachine>().FromNew().AsSingle();
            Container.Bind<BootstrapState>().FromNew().AsSingle();
            Container.Bind<LoadLevelState>().FromNew().AsSingle();
            Container.Bind<GameLoopState>().FromNew().AsSingle();
        }
        private void BindFactories()
        {
            Container.Bind<IUIFactory>().To<UIFactory>().FromNew().AsSingle();
            Container.Bind<IGameObjectsFactory>().To<GameObjectsFactory>().AsSingle();
            Container.Bind<IZenjectFactory>().To<ZenjectFactory>().AsSingle();
        }
    }
}
