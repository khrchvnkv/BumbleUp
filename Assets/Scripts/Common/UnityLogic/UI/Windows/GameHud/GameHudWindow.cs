using Common.Infrastructure.Services.Score;
using TMPro;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.UI.Windows.GameHud
{
    public sealed class GameHudWindow : WindowBase<GameHudWindowData>
    {
        [SerializeField] private TMP_Text _scoreText;

        private IScoreService _scoreService;

        [Inject]
        private void Construct(IScoreService scoreService) => _scoreService = scoreService;
        
        protected override void PrepareForShowing()
        {
            base.PrepareForShowing();
            _scoreService.OnScoreChanged += UpdateScoreText;
        }
        protected override void PrepareForHiding()
        {
            base.PrepareForHiding();
            _scoreService.OnScoreChanged -= UpdateScoreText;
        }
        private void UpdateScoreText(uint newScore) => _scoreText.text = newScore.ToString();
    }
}