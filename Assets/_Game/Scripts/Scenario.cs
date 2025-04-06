using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class Scenario : MonoBehaviour
    {
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private float _gameTime = 2f * 60f;
        private void Start()
        {
            StartCoroutine(Go());
        }

        private IEnumerator Go()
        {
            var elapsedTime = 0f;
            
            while (elapsedTime < _gameTime)
            {
                elapsedTime += Time.deltaTime;
                _progressSlider.value = elapsedTime / _gameTime;
                yield return null;
            }
        }
    }
}