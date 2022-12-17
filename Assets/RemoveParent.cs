using UnityEngine;

namespace Scarecrow
{
    class RemoveParent : MonoBehaviour
    {
        public void Do() => transform.parent = null;
    }
}
