using System.Collections;
using System.Collections.Generic;
using Common.Infrastructure.Factories.GameObjectsFactory;
using Common.Infrastructure.Services.Coroutines;
using Common.Infrastructure.Services.StaticData;
using Common.StaticData;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.StairsLogic
{
    public sealed class ObstacleMovement : MonoBehaviour
    {
        [SerializeField, CurveRange(0, 0, 1, 2, EColor.Orange)] private AnimationCurve _jumpCurve;
        [SerializeField] private float _rotationByJumpDegrees;
        
        private ObstacleStaticData _obstacleStaticData;
        private IGameObjectsFactory _gameObjectsFactory;
        private ICoroutineRunner _coroutineRunner;

        private Coroutine _coroutine;

        private float LerpSpeed => 1.0f / _obstacleStaticData.JumpDuration;
        
        [Inject]
        private void Construct(IStaticDataService staticDataService, IGameObjectsFactory gameObjectsFactory, 
            ICoroutineRunner coroutineRunner)
        {
            _obstacleStaticData = staticDataService.GameStaticData.ObstacleStaticData;
            _gameObjectsFactory = gameObjectsFactory;
            _coroutineRunner = coroutineRunner;
        }
        public void SetPath(List<Vector3> points)
        {
            _coroutineRunner.StopCoroutineSafe(_coroutine);
            _coroutine = _coroutineRunner.StartCoroutine(MoveCoroutine(points));
        }
        private IEnumerator MoveCoroutine(List<Vector3> points)
        {
            var obstacleTransform = transform;
            foreach (var point in points)
            {
                var startPoint = obstacleTransform.position;
                var startHorizontal = startPoint;
                startHorizontal.y = point.y;
                var height = Mathf.Abs(startPoint.y - point.y);
                var startRotation = obstacleTransform.rotation;
                var startRotationEuler = startRotation.eulerAngles;
                var endRotation = Quaternion.Euler(startRotationEuler.x + _rotationByJumpDegrees, startRotationEuler.y,
                    startRotationEuler.z);
                
                float t = 0.0f;
                while (t < 1.0f)
                {
                    t += LerpSpeed * Time.deltaTime;
                    t = Mathf.Clamp01(t);
                    var verticalMovement = new Vector3(0.0f, height * _jumpCurve.Evaluate(t), 0.0f);
                    var horizontalMovement = Vector3.Lerp(startHorizontal, point, t);
                    obstacleTransform.position = horizontalMovement + verticalMovement;
                    var rotationQuaternion = Quaternion.Lerp(startRotation, endRotation, t);
                    obstacleTransform.rotation = startRotation * rotationQuaternion;
                    
                    yield return null;
                }
            }
            
            _gameObjectsFactory.DespawnObstacle(this);
        }
    }
}