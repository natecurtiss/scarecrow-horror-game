using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Scarecrow
{
    class CloseEyes : MonoBehaviour
    {
        [SerializeField] float _delay = 4f;
        [SerializeField] float _duration = 1f;
        
        public void Do() => StartCoroutine(Delay());

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(_delay);
            GetComponent<Image>().DOFade(1f, _duration);
        }
    }
}
