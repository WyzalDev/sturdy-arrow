using static UnityEngine.InputSystem.InputAction;

namespace SturdyArrow.Services
{
    public class DefaultInputService : IInputService
    {
        private readonly InputControls _controls;

        private bool? _enabled = null;

        public float UpAxis { get; private set; }
        public bool enabled
        {
            get => _enabled.HasValue ? _enabled.Value : false;
            set
            {
                if(_enabled != value)
                {
                    if(value)
                        _controls.Enable();
                    else if(_enabled.HasValue)
                        _controls.Disable();
                    _enabled = value;
                }
            }
        }

        public DefaultInputService()
        {
            _controls = new InputControls();
            enabled = true;
            SubscribeOnControls();
        }

        ~DefaultInputService()
        {
            UnsubscribeOnControls();
            enabled = false;
        }
        private void SubscribeOnControls()
        {
            _controls.Player.Up.performed += OnUp;
            _controls.Player.Up.canceled += OnUp;
        }

        private void UnsubscribeOnControls()
        {
            _controls.Player.Up.performed -= OnUp;
            _controls.Player.Up.canceled -= OnUp;
        }

        #region ADAPTER_METHODS
        private void OnUp(CallbackContext ctx) => UpAxis = ctx.ReadValue<float>();
        #endregion
    }
}