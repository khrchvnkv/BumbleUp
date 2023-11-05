using System;

namespace Common.Infrastructure.Services.Input
{
    public interface IInputService
    {
        event Action OnTouch;
        event Action OnLeftSwipe;
        event Action OnRightSwipe;
        
        void ActivateInput();
        void DeactivateInput();
    }
}