using UnityEngine;
using UnityEngine.Events;

namespace Scarecrow
{
    class PickingUp : Toggleable
    {
        Pickup _current;
        [SerializeField] bool _debug;
        [SerializeField] LayerMask _pickupLayer;
        [SerializeField] Transform _camera;
        [SerializeField] KeyCode _key = KeyCode.E;
        [SerializeField] float _range = 2f;
        [SerializeField] UnityEvent _onHover;
        [SerializeField] UnityEvent _onPickUp;
        [SerializeField] UnityEvent _onLastPickup;

        void OnDrawGizmos()
        {
            if (_debug) 
                Debug.DrawRay(_camera.position, _camera.forward * _range, Color.red);
        }

        void Update()
        {
            if (IsEnabled)
            {
                var hover = Physics.Raycast(_camera.position, _camera.forward, out var hit, _range, _pickupLayer);
                if (_current != null && (!hover || _current != hit.collider.GetComponent<Pickup>()))
                    _current.StopHover();
                if (hover)
                {
                    _current = hit.collider.GetComponent<Pickup>();
                    if (Input.GetKeyDown(_key))
                    {
                        _onPickUp?.Invoke();
                        _current.Take();
                        Pickup.Amount--;
                        if (Pickup.Amount == 0)
                        {
                            _onLastPickup.Invoke();
                        }
                        Destroy(hit.collider.gameObject);
                    }
                    else
                    {
                        _onHover?.Invoke();
                        _current.StartHover();
                    }
                }
            }
        }
    }
}
