using DG.Tweening;
using UnityEngine;

namespace Scarecrow
{
    class CameraFix : MonoBehaviour
    {
        [SerializeField] float _duration = 0.5f;
        
        public void Fix()
        {
            var rot = transform.eulerAngles;
            rot = new(0f, rot.y, rot.z);
            transform.eulerAngles = rot;
        }
    }
}
