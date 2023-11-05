using Common.Infrastructure.WindowsManagement;

namespace Common.Infrastructure.Factories.UIFactory
{
    public interface IUIFactory
    {
        void CreateUIRoot();
        void ShowLoadingCurtain();
        void HideLoadingCurtain();
        void Show<TData>(TData data) where TData : struct, IWindowData;
        void Hide<TData>() where TData : struct, IWindowData;
    }
}