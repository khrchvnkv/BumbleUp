using System;
using Common.Infrastructure.Services.MonoUpdate;
using UnityEngine;

namespace Common.Infrastructure.Services.Input
{
    public class MobileInputService : IInputService, IDisposable
    {
        private const int InputButtonIndex = 0;
        private const float SwipeThresholdScreenPercent = 0.3f;
        
        public event Action OnTouch;
        public event Action OnLeftSwipe;
        public event Action OnRightSwipe;
        
        private readonly IUpdateService _updateService;
        
        protected bool IsActive;
        private Vector3 _touchPosition; 

        public MobileInputService(IUpdateService updateService)
        {
            _updateService = updateService;
            _updateService.OnUpdate += UpdateInput;
        }
        
        public void ActivateInput() => IsActive = true;
        public void DeactivateInput() => IsActive = false;
        public void Dispose() => _updateService.OnUpdate -= UpdateInput;
        protected virtual void UpdateInput()
        {
            if (!IsActive) return;

            if (UnityEngine.Input.GetMouseButtonDown(InputButtonIndex))
            {
                _touchPosition = UnityEngine.Input.mousePosition;
                return;
            }

            if (UnityEngine.Input.GetMouseButtonUp(InputButtonIndex))
            {
                var upPosition = UnityEngine.Input.mousePosition;
                var xOffset =  upPosition.x - _touchPosition.x;
                if (Mathf.Abs(xOffset) <= Screen.width * SwipeThresholdScreenPercent)
                {
                    Touch();
                }
                else
                {
                    if (xOffset > 0.0f) RightSwipe();
                    else LeftSwipe();
                }
            }
        }

        protected void Touch() => OnTouch?.Invoke();
        protected void LeftSwipe() => OnLeftSwipe?.Invoke();
        protected void RightSwipe() => OnRightSwipe?.Invoke();
    }
}