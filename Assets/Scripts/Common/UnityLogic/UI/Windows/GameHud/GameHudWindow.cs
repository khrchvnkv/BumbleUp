using TMPro;
using UnityEngine;

namespace Common.UnityLogic.UI.Windows.GameHud
{
    public sealed class GameHudWindow : WindowBase<GameHudWindowData>
    {
        [SerializeField] private TMP_Text _scoreText;
        
        protected override void PrepareForShowing()
        {
            base.PrepareForShowing();
            WindowData.ScoreService.OnScoreChanged += UpdateScoreText;
        }
        protected override void PrepareForHiding()
        {
            base.PrepareForHiding();
            WindowData.ScoreService.OnScoreChanged -= UpdateScoreText;
        }
        private void UpdateScoreText(uint newScore) => _scoreText.text = newScore.ToString();
    }
}