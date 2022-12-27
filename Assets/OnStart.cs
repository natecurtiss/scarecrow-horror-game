using UnityEngine;
using UnityEngine.Events;

namespace Scarecrow
{
    class OnStart : MonoBehaviour
    {
        [SerializeField] UnityEvent _on;

        void Start() => _on.Invoke();
    }
}
