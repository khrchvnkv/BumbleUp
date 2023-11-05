using Common.Infrastructure.WindowsManagement;

namespace Common.UnityLogic.UI.Windows.Loss
{
    public struct LossWindowData : IWindowData
    {
        public string WindowName => "LossWindow";
        public bool DestroyOnClosing => false;
    }
}