using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scarecrow
{
    class CloseEyes : MonoBehaviour
    {
        [SerializeField] float _delay = 4f;
        [SerializeField] float _duration = 1f;
        [SerializeField] float _to = 1f;
        [SerializeField] UnityEvent _onDone;
        
        public void Do() => StartCoroutine(Delay());

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(_delay);
            GetComponent<Image>().DOFade(_to, _duration);
            yield return new WaitForSeconds(_duration);
            _onDone.Invoke();
        }
    }
}
