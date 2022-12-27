using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Scarecrow
{
    class TextFade : MonoBehaviour
    {
        [SerializeField] float _delay;
        [SerializeField] float _duration = 1f;

        TextMeshProUGUI _text;

        void Awake() => _text = GetComponent<TextMeshProUGUI>();

        IEnumerator Start()
        {
            _text.color = new(_text.color.r, _text.color.g, _text.color.b, 0f);
            yield return new WaitForSeconds(_delay);
            _text.DOFade(1f, _duration);
        }
    }
}
