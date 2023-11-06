using System;
using System.Collections;
using Common.Infrastructure.Factories.UIFactory;
using Common.Infrastructure.Services.Coroutines;
using Common.Infrastructure.Services.Input;
using Common.Infrastructure.Services.SceneContext;
using Common.Infrastructure.Services.Score;
using Common.Infrastructure.Services.StaticData;
using Common.StaticData;
using Common.UnityLogic.UI.Windows.Loss;
using Common.UnityLogic.UI.Windows.TapToStart;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Ball
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallMovement : MonoBehaviour
    {
        public event Action BallFallingStarted;
        public event Action BallFallingCompleted;
        
        [Header("Components")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private LayerMask _groundLayer;

        [Header("Curves")]
        [SerializeField, CurveRange(0, 0, 1, 2, EColor.Orange)] private AnimationCurve _forwardJumpCurve;
        [SerializeField, CurveRange(0, 0, 1, 1, EColor.Blue)] private AnimationCurve _sideJumpCurve;

        private ISceneContextService _sceneContextService;
        private IInputService _inputService;
        private ICoroutineRunner _coroutineRunner;
        private BallStaticData _ballStaticData;
        private IScoreService _scoreService;

        private Coroutine _sideJumpCoroutine;
        private Coroutine _forwardJumpCoroutine;

        private float LerpSpeed => 1.0f / _ballStaticData.JumpDuration;

        [Inject]
        private void Construct(ISceneContextService sceneContextService, IInputService inputService, 
            ICoroutineRunner coroutineRunner, IStaticDataService staticDataService, IScoreService scoreService)
        {
            _sceneContextService = sceneContextService;
            _inputService = inputService;
            _coroutineRunner = coroutineRunner;
            _ballStaticData = staticDataService.GameStaticData.BallStaticData;
            _scoreService = scoreService;
        }
        public void ResetMovement()
        {
            _rigidbody.position = Vector3.zero;
            _sceneContextService.Camera.MoveToTarget(_rigidbody.position);
            _sideJumpCoroutine = null;
            _forwardJumpCoroutine = null;
        }
        private void OnValidate() => _rigidbody ??= gameObject.GetComponent<Rigidbody>();
        private void OnEnable()
        {
            _inputService.OnTouch += Jump;
            _inputService.OnLeftSwipe += MoveLeft;
            _inputService.OnRightSwipe += MoveRight;
        }
        private void OnDisable()
        {
            _inputService.OnTouch -= Jump;
            _inputService.OnLeftSwipe -= MoveLeft;
            _inputService.OnRightSwipe -= MoveRight;
        }
        private void Jump() => TryForwardJump();
        private void MoveLeft() => TrySideJump(Vector3.left);
        private void MoveRight() => TrySideJump(Vector3.right);
        private void TryForwardJump()
        {
            if (_sideJumpCoroutine is null)
            {
                bool isJumpCompleted = _forwardJumpCoroutine is null;
                _coroutineRunner.StopCoroutineSafe(_forwardJumpCoroutine);

                if (!isJumpCompleted)
                {
                    var nextPoint = _sceneContextService.Stairs.NextStepBallPivot.position;
                    _sceneContextService.Stairs.UpdateStairs();
                    _sceneContextService.Camera.SetNewTarget(nextPoint);
                    _scoreService.AddScorePoint();
                }

                _forwardJumpCoroutine = _coroutineRunner.StartCoroutine(ForwardJumpCoroutine());
            }
        }
        private void TrySideJump(Vector3 movementDirection)
        {
            if (_sideJumpCoroutine is null && _forwardJumpCoroutine is null)
            {
                _sideJumpCoroutine = _coroutineRunner.StartCoroutine(SideJumpCoroutine(movementDirection));
            }
        }
        private IEnumerator ForwardJumpCoroutine()
        {
            var oldHorizontal = _rigidbody.position;
            var nextPoint = _sceneContextService.Stairs.NextStepBallPivot.position;
            var multiplier = nextPoint.y - oldHorizontal.y;
            var newPosition = new Vector3(oldHorizontal.x, nextPoint.y, nextPoint.z);
            var newHorizontal = newPosition;
            newHorizontal.y = oldHorizontal.y;
            yield return Jump(oldHorizontal, newHorizontal, _forwardJumpCurve, multiplier);

            _rigidbody.MovePosition(newPosition);
            _sceneContextService.Stairs.UpdateStairs();
            _sceneContextService.Camera.SetNewTarget(nextPoint);
            _scoreService.AddScorePoint();
            _forwardJumpCoroutine = null;
        }
        private IEnumerator SideJumpCoroutine(Vector3 movementDirection)
        {
            var oldHorizontal = _rigidbody.position;
            var newHorizontal = oldHorizontal + _ballStaticData.HorizontalOffset * movementDirection.normalized;
            yield return Jump(oldHorizontal, newHorizontal, _sideJumpCurve, _ballStaticData.SideJumpHeight);

            _rigidbody.MovePosition(newHorizontal);
            _sideJumpCoroutine = IsGrounded() ? null : _coroutineRunner.StartCoroutine(FallCoroutineAndShowLossWindow());
        }
        private IEnumerator Jump(Vector3 oldHorizontal, Vector3 newHorizontal, AnimationCurve jumpCurve, float jumpMultiplier)
        {
            float t = 0.0f;
            while (t < 1.0f)
            {
                t += LerpSpeed *  Time.deltaTime;
                t = Mathf.Clamp01(t);
                var lerpHorizontalPosition = Vector3.Lerp(oldHorizontal, newHorizontal, t);
                var verticalMovement = new Vector3(0, jumpMultiplier * jumpCurve.Evaluate(t), 0);
                _rigidbody.MovePosition(lerpHorizontalPosition + verticalMovement);
                
                yield return null;
            }
        }
        private IEnumerator FallCoroutineAndShowLossWindow()
        {
            BallFallingStarted?.Invoke();
            
            float t = 0.0f;
            var startPosition = _rigidbody.position;
            var endPosition = startPosition + Vector3.down * Physics.gravity.magnitude;
            while (t < 1.0f)
            {
                t += _ballStaticData.FallingSpeed * Time.deltaTime;
                _rigidbody.position = Vector3.Lerp(startPosition, endPosition, t);
                yield return null;
            }
            
            BallFallingCompleted?.Invoke();
        }
        private bool IsGrounded() =>
            Physics.RaycastAll(_rigidbody.position, Vector3.down, 1.0f, _groundLayer).Length > 0;
    }
}