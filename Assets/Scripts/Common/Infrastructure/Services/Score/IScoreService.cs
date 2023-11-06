using System;

namespace Common.Infrastructure.Services.Score
{
    public interface IScoreService
    {
        event Action<uint> OnScoreChanged;
        
        uint Score { get; }

        void ResetScoreCalculating();
        void AddScorePoint();
    }
}