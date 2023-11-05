using System;
using System.Collections.Generic;
using Common.Infrastructure.Factories.Zenject;
using Common.Infrastructure.Services.AssetsManagement;
using Common.Infrastructure.Services.StaticData;
using Common.Infrastructure.WindowsManagement;
using Common.UnityLogic.UI.Windows;
using Object = UnityEngine.Object;

namespace Common.Infrastructure.Factories.UIFactory
{
    public sealed class UIFactory : IUIFactory
    {
        private const string UI_PATH = "UI/{0}";

        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IZenjectFactory _zenjectFactory;

        private readonly Dictionary<Type, WindowInfo> _createdObjects;

        private UIRoot _uiRoot;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, IZenjectFactory zenjectFactory)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _zenjectFactory = zenjectFactory;
            _createdObjects = new();
        }
        public void CreateUIRoot()
        {
            if (_uiRoot is not null) Object.Destroy(_uiRoot.gameObject);

            var prefab = _staticDataService.GameStaticData.WindowStaticData.UIRoot;
            _uiRoot = _zenjectFactory.Instantiate(prefab);
        }
        public void ShowLoadingCurtain()
        {
            if (_uiRoot.LoadingCurtain is null)
            {
                var prefab = _staticDataService.GameStaticData.WindowStaticData.LoadingCurtainPrefab;
                _uiRoot.LoadingCurtain = _zenjectFactory.Instantiate(prefab, _uiRoot.transform);
            }
            _uiRoot.LoadingCurtain.Show();
        }
        public void HideLoadingCurtain() => _uiRoot.LoadingCurtain.Hide();
        public void Show<TData>(TData data) where TData : struct, IWindowData
        {
            var dataType = typeof(TData);
            if (!_createdObjects.TryGetValue(dataType, out var windowInfo))
            {
                var path = string.Format(UI_PATH, data.WindowName);
                var windowPrefab = _assetProvider.Load(path);
                var windowInstance = _zenjectFactory.Instantiate(windowPrefab, _uiRoot.WindowsParent);
                var window = windowInstance.GetComponent<IWindow>();
                windowInfo = new WindowInfo(window, data);
                _createdObjects.Add(dataType, windowInfo);
            }

            windowInfo.Window.Show(data);
        }

        public void Hide<TData>() where TData : struct, IWindowData
        {
            var windowType = typeof(TData);
            if (_createdObjects.TryGetValue(windowType, out var windowInfo))
            {
                if (windowInfo.WindowData.DestroyOnClosing) _createdObjects.Remove(windowType);
                windowInfo.Window.Hide();
            }
        }
    }
}