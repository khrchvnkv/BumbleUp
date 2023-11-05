using Common.Infrastructure.WindowsManagement;
using UnityEngine;

namespace Common.UnityLogic.UI.Windows
{
    public interface IWindow
    {
        GameObject GameObject { get; }
        void Show(IWindowData windowData);
        void Hide();
    }
}