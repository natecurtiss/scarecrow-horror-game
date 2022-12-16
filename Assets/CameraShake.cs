using DG.Tweening;
using UnityEngine;

namespace Scarecrow
{
    class CameraShake : MonoBehaviour
    {
        [SerializeField] float _duration;
        [SerializeField] float _posStrength = 1f;
        [SerializeField] float _rotStrength = 15f;

        public void Do()
        {
            transform.DOShakePosition(_duration, _posStrength).SetEase(Ease.InQuad);
            transform.DOShakeRotation(_duration, _rotStrength).SetEase(Ease.InQuad);
        }
    }
}
