using Common.Infrastructure.Services.Score;
using Common.Infrastructure.WindowsManagement;

namespace Common.UnityLogic.UI.Windows.GameHud
{
    public struct GameHudWindowData : IWindowData
    {
        public readonly IScoreService ScoreService;

        public GameHudWindowData(IScoreService scoreService)
        {
            ScoreService = scoreService;
        }

        public string WindowName => "GameHUD";
        public bool DestroyOnClosing => false;
    }
}