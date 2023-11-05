using Common.Infrastructure.WindowsManagement;

namespace Common.UnityLogic.UI.Windows.TapToStart
{
    public struct TapToStartWindowData : IWindowData
    {
        public string WindowName => "TapToStartWindow";
        public bool DestroyOnClosing => true;
    }
}