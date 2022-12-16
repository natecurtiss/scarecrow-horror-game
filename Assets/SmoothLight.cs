using DG.Tweening;
using UnityEngine;

namespace Scarecrow
{
    class SmoothLight : MonoBehaviour
    {
        Light _light;
        float _intensity;

        [SerializeField] float _on = 0.2f;
        [SerializeField] float _off = 0.2f;

        void Awake()
        {
            _light = GetComponent<Light>();
            _intensity = _light.intensity;
            _light.intensity = 0f;
        }

        public void On()
        {
            DOTween.To(() => _light.intensity, v => _light.intensity = v, _intensity, _on);
        }

        public void Off()
        {
            DOTween.To(() => _light.intensity, v => _light.intensity = v, 0, _off);
        }
    }
}
