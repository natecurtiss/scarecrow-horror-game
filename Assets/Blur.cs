using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Serialization;

namespace Scarecrow
{
    class Blur : MonoBehaviour
    {
        PostProcessVolume _volume;
        [FormerlySerializedAs("_duration"),SerializeField] float _in = 1f;
        [SerializeField] float _out = 1f;
        [SerializeField] bool _hasEnd = true;

        void Awake() => _volume = GetComponent<PostProcessVolume>();

        IEnumerator Start()
        {
            DOTween.To(() => _volume.weight, w => _volume.weight = w, 1f, _in);
            if (_hasEnd)
            {
                yield return new WaitForSeconds(_in);
                DOTween.To(() => _volume.weight, w => _volume.weight = w, 0f, _out);
            }
        }
    }
}
