using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.UnityLogic.StairsLogic
{
    public sealed class Step : MonoBehaviour
    {
        [SerializeField] private Transform[] _obstaclesSpawnPoints;
        [field: SerializeField] public Transform BallPivot { get; private set; }
        [field: SerializeField] public Transform NextPivot { get; private set; }

        private void OnValidate()
        {
            if (_obstaclesSpawnPoints.Length == 0) Debug.LogError("It is necessary to specify obstacle spawn points");
        }
        public Transform GetRandomObstacleSpawnPoint()
        {
            var randomIndex = Random.Range(0, _obstaclesSpawnPoints.Length);
            return _obstaclesSpawnPoints[randomIndex];
        }
    }
}