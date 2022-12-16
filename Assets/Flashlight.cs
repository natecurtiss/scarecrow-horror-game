using UnityEngine;
using UnityEngine.Events;

namespace Scarecrow
{
    class Flashlight : Toggleable
    {
        [SerializeField] float _startBatteryLife = 10f;
        [SerializeField] UnityEvent _onClickAndOutOfBattery;
        [SerializeField] UnityEvent<float> _onBatteryDrain;
        [SerializeField] UnityEvent _onBatteryReplenish;
        [SerializeField] UnityEvent _onRunOutOfBattery;
        [SerializeField] UnityEvent _onTurnOn;
        [SerializeField] UnityEvent _onTurnOff;

        bool _isOn;
        float _batteryLife;

        void Awake() => _batteryLife = _startBatteryLife;

        void Update()
        {
            if (!IsEnabled)
                return;
            if (Input.GetMouseButtonDown(0))
            {
                if (_batteryLife <= 0f)
                {
                    _onClickAndOutOfBattery.Invoke();
                }
                else
                {
                    _isOn = !_isOn;
                    (_isOn ? _onTurnOn : _onTurnOff).Invoke();
                }
                
            }

            if (_isOn && _batteryLife > 0f)
            {
                _batteryLife -= Time.deltaTime;
                _onBatteryDrain.Invoke(Mathf.Max(0, _batteryLife) / _startBatteryLife);
                if (_batteryLife <= 0f)
                {
                    _onRunOutOfBattery.Invoke();
                    _isOn = false;
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Battery>(out var b))
            {
                b.Use();
                _batteryLife = _startBatteryLife;
                _onBatteryReplenish.Invoke();
            }
        }

        public void Off() => _onTurnOff.Invoke();
        public void Charge(float to) => _batteryLife = to;
    }
}
