using Common.Infrastructure.Services.MonoUpdate;
using UnityEngine;

namespace Common.Infrastructure.Services.Input
{
    public sealed class StandaloneInputService : MobileInputService
    {
        public StandaloneInputService(IUpdateService updateService) : base(updateService)
        { }

        protected override void UpdateInput()
        {
            if (!IsActive) return;
            
            if (IsKeyDown(KeyCode.A))
            {
                LeftSwipe();
                return;
            }
            
            if (IsKeyDown(KeyCode.D))
            {
                RightSwipe();
                return;
            }
            
            if (IsKeyDown(KeyCode.W))
            {
                Touch();
                return;
            }
            
            base.UpdateInput();
        }
        private bool IsKeyDown(KeyCode keyCode) => UnityEngine.Input.GetKeyUp(keyCode);
    }
}