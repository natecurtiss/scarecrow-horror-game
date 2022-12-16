using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Input;
using static UnityEngine.Time;

namespace Scarecrow
{
    class PlayerMovement : Toggleable
    {
        CharacterController _controller;
        float _xRotation;
        float _yRotation;

        [SerializeField] float _moveSpeed = 10f;
        [SerializeField] float _lookSensitivity = 100f;
        [SerializeField] Transform _camera;
        [SerializeField] UnityEvent _onBreathe;
        [SerializeField] UnityEvent _onWalk;

        public Vector3 Direction { get; private set; }
        
        void Awake() => _controller = GetComponent<CharacterController>();

        void Update()
        {
            if (IsEnabled)
            {
                if (_controller.velocity.normalized != Vector3.zero) 
                    Direction = _controller.velocity.normalized;
                Move();
                Look();
            }
        }

        void Move()
        {
            var input = new Vector3(GetAxisRaw("Horizontal"), 0, GetAxisRaw("Vertical")).normalized;
            if (input == Vector3.zero)
                _onBreathe.Invoke();
            else
                _onWalk.Invoke();
            var rot = Quaternion.Euler(0, _camera.eulerAngles.y, 0);
            var move = rot * (input * (_moveSpeed * deltaTime));
            _controller.Move(move);
        }

        void Look()
        {
            var speed = _lookSensitivity * deltaTime;
            var input = new Vector2(GetAxis("Mouse X"), GetAxisRaw("Mouse Y")) * speed;
            
            _xRotation = Mathf.Clamp(_xRotation - input.y, -90, 90);
            _yRotation += input.x;
            
            _camera.localRotation = Quaternion.Euler(_xRotation, _camera.eulerAngles.y, _camera.eulerAngles.z);
            _camera.localRotation = Quaternion.Euler(_camera.eulerAngles.x, _yRotation, _camera.eulerAngles.z);
        }

        public override void Enable()
        {
            base.Enable();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public override void Disable()
        {
            base.Disable();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

