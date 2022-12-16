﻿using UnityEngine;
using UnityEngine.Events;

namespace Scarecrow
{
    class Pickup : MonoBehaviour
    {
        [SerializeField] UnityEvent _onStartHover;
        [SerializeField] UnityEvent _onStopHover;
        [SerializeField] UnityEvent _onPickUp;
        
        public void StartHover() => _onStartHover?.Invoke();
        public void StopHover() => _onStopHover?.Invoke();
        public void Take() => _onPickUp?.Invoke();
    }
}