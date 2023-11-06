using System;

namespace Common.Infrastructure.Services.Score
{
    public interface IScoreService
    {
        event Action<uint> OnScoreChanged;

        void ResetScoreCalculating();
        void AddScorePoint();
    }
}