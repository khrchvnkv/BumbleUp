using System;

namespace Common.Infrastructure.Services.Score
{
    public sealed class ScoreService : IScoreService
    {
        public event Action<uint> OnScoreChanged;
        
        private uint _score;

        public void ResetScoreCalculating()
        {
            _score = 0;
            ScoreUpdated();
        }
        public void AddScorePoint()
        {
            _score++;
            ScoreUpdated();
        }
        private void ScoreUpdated() => OnScoreChanged?.Invoke(_score);
    }
}