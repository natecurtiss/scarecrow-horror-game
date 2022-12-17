using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Scarecrow
{
    class Slide : MonoBehaviour
    {
        Slider _slider;
        [SerializeField] float _duration = 0.1f;

        void Awake() => _slider = GetComponent<Slider>();

        public void Do(float val) => _slider.DOValue(val, _duration);
    }
}
