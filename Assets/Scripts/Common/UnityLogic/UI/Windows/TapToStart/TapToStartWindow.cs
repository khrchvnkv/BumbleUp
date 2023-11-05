using Common.Infrastructure.Factories.UIFactory;
using Common.Infrastructure.Services.Input;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common.UnityLogic.UI.Windows.TapToStart
{
    public sealed class TapToStartWindow : WindowBase<TapToStartWindowData>
    {
        private IInputService _inputService;
        private IUIFactory _uiFactory;
        
        [Inject]
        private void Construct(IInputService inputService, IUIFactory uiFactory)
        {
            _inputService = inputService;
            _uiFactory = uiFactory;
        }

        protected override void PrepareForShowing()
        {
            base.PrepareForShowing();
            _inputService.ActivateInput();
            _inputService.OnTouch += HideTapToStartWindow;
        }
        protected override void PrepareForHiding()
        {
            base.PrepareForHiding();
            _inputService.OnTouch -= HideTapToStartWindow;
        }
        private void HideTapToStartWindow() => _uiFactory.Hide<TapToStartWindowData>();
    }
}