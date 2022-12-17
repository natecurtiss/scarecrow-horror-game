using UnityEngine;

namespace Scarecrow
{
    class HUD : Toggleable
    {
        [SerializeField] Canvas _canvas;
        
        void Update()
        {
            if (!IsEnabled)
                return;
            if (Input.GetKeyDown(KeyCode.Tab))
                _canvas.enabled = true;
            else if (Input.GetKeyUp(KeyCode.Tab))
                _canvas.enabled = false;
        }

        public override void Disable()
        {
            base.Disable();
            _canvas.enabled = false;
        }
    }
}
