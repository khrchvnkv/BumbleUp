using Common.Infrastructure.WindowsManagement;

namespace Common.UnityLogic.UI.Windows
{
    public sealed class WindowInfo
    {
        public readonly IWindow Window;
        public readonly IWindowData WindowData;

        public WindowInfo(IWindow window, IWindowData windowData)
        {
            Window = window;
            WindowData = windowData;
        }
    }
}