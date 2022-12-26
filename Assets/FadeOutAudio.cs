using DG.Tweening;
using UnityEngine;

namespace Scarecrow
{
    class FadeOutAudio : MonoBehaviour
    {
        [SerializeField] AudioSource _source;
        [SerializeField] float _on = 1f;
        [SerializeField] float _out = 0.1f;

        public void Play()
        {
            _source.DOKill();
            _source.volume = _on;
            _source.Play();
        }

        public void Stop() => _source.DOFade(0f, _out);
        public void Stop(float delay) => DOTween.Sequence().AppendInterval(delay).Append(_source.DOFade(0f, _out)).Play();
    }
}