using System.Collections.Generic;
using System.Linq;
using NTC.Pool;
using UnityEngine;

namespace Common.UnityLogic.StairsLogic
{
    public sealed class Stairs : MonoBehaviour
    {
        private const int CurrentStepIndex = 6;

        [SerializeField] private Step _stepPrefab;
        [SerializeField, Min(1)] private int _stepsCount;
        [SerializeField] private Transform _startStepsSpawnPoint;
        [SerializeField] private Vector3 _offsetSpawnVector;

        private List<Step> _allSteps = new();

        public Transform CurrentStepBallPivot => _allSteps[CurrentStepIndex].BallPivot;
        public Transform NextStepBallPivot => _allSteps[CurrentStepIndex + 1].BallPivot;

        public void RebuildSteps()
        {
            foreach (var step in _allSteps)
            {
                NightPool.Despawn(step);
            }
            _allSteps.Clear();

            for (int i = 0; i < _stepsCount; i++)
            {
                var step = NightPool.Spawn(_stepPrefab, _startStepsSpawnPoint.position + i * _offsetSpawnVector,
                    _startStepsSpawnPoint.rotation, _startStepsSpawnPoint);
                _allSteps.Add(step);
            }
        }
        public void UpdateStairs()
        {
            var firstStep = _allSteps.First();
            _allSteps.RemoveAt(0);
            var lastStep = _allSteps.Last();
            firstStep.transform.position = lastStep.NextPivot.position;
            _allSteps.Add(firstStep);
        }
    }
}