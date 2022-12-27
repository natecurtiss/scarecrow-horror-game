using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scarecrow
{
    class FromMenu : MonoBehaviour
    {
        bool _hitSpace;
        [SerializeField] Image _panel;
        [SerializeField] float _duration = 1f;
        [SerializeField] string _map = "Map";
        
        void Update()
        {
            if (_hitSpace)
                return;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _hitSpace = true;
                StartCoroutine(Change());
            }
        }

        IEnumerator Change()
        {
            _panel.DOFade(1f, _duration);
            yield return new WaitForSeconds(_duration);
            SceneManager.LoadScene(_map);
        }
    }
}
