using Common.Infrastructure.WindowsManagement;

namespace Common.UnityLogic.UI.Windows.GameHud
{
    public struct GameHudWindowData : IWindowData
    {
        public string WindowName => "GameHUD";
        public bool DestroyOnClosing => false;
    }
}