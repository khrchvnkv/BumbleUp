using Common.Infrastructure.WindowsManagement;

namespace Common.UnityLogic.UI.Windows.Loss
{
    public struct LossWindowData : IWindowData
    {
        public readonly uint CurrentResult;
        public readonly uint BestResult;

        public LossWindowData(uint currentResult, uint bestResult)
        {
            CurrentResult = currentResult;
            BestResult = bestResult;
        }

        public string WindowName => "LossWindow";
        public bool DestroyOnClosing => false;
    }
}