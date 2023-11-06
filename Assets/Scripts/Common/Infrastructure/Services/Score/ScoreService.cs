using System;

namespace Common.Infrastructure.Services.Score
{
    public sealed class ScoreService : IScoreService
    {
        public event Action<uint> OnScoreChanged;
        
        public uint Score { get; private set; }

        public void ResetScoreCalculating()
        {
            Score = 0;
            ScoreUpdated();
        }
        public void AddScorePoint()
        {
            Score++;
            ScoreUpdated();
        }
        private void ScoreUpdated() => OnScoreChanged?.Invoke(Score);
    }
}