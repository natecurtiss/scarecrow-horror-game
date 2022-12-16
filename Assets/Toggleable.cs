using UnityEngine;

namespace Scarecrow
{
    abstract class Toggleable  : MonoBehaviour
    {
        [SerializeField] bool _enableOnStart = true;
        protected bool IsEnabled { get; private set; }
        
        protected virtual void Start()
        {
            if (_enableOnStart)
                Enable();
        }

        public virtual void Enable() => IsEnabled = true;
        public virtual void Disable() => IsEnabled = false;
    }
}