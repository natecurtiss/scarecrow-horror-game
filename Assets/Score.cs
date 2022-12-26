using System.Collections;
using TMPro;
using UnityEngine;

namespace Scarecrow
{
    class Score : MonoBehaviour
    {
        int _total;
        TextMeshProUGUI _text;

        void Awake() => _text = GetComponent<TextMeshProUGUI>();

        IEnumerator Start()
        {
            yield return null;
            _total = Pickup.Amount;
            _text.text = $"0/{_total}";
        }

        void Update() => _text.text = $"{_total - Pickup.Amount}/{_total}";
    }
}