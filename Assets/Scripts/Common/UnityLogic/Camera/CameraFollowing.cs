using Common.Infrastructure.Services.MonoUpdate;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Camera
{
    public sealed class CameraFollowing : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _lerpSpeed;
        [SerializeField] private Vector3 _offset;

        private IUpdateService _updateService;
        private Vector3? _target;

        [Inject]
        private void Construct(IUpdateService updateService) => _updateService = updateService;
        private void OnValidate() => _transform ??= transform;
        private void OnEnable() => _updateService.OnLateUpdate += UpdateCameraPosition;
        private void OnDisable() => _updateService.OnLateUpdate -= UpdateCameraPosition;
        
        private void UpdateCameraPosition()
        {
            if (!_target.HasValue) return;

            var target = _target.Value + _offset;
            _transform.position = Vector3.Lerp(_transform.position, target, _lerpSpeed * Time.deltaTime);
        }
        public void MoveToTarget(Vector3 newPosition)
        {
            _target = null;
            _transform.position = newPosition + _offset;
        }
        public void SetNewTarget(Vector3 newTarget) => _target = newTarget;
    }
}