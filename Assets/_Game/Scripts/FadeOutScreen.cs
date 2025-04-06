using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class FadeOutScreen : MonoBehaviour
    {
        [SerializeField] private Image _background;

        private void Start()
        {
            StartCoroutine(FadeOut(0.3f));
        }
        
        private IEnumerator FadeOut(float duration)
        {
            _background.color = Color.black;
            _background.DOFade(0, duration);
            yield return new WaitForSeconds(duration);
            _background.gameObject.SetActive(false);
        }
    }
}