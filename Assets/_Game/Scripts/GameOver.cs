using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private Health _characterHealth;
        [SerializeField] private Image _fadeInScreen;

        private void Start()
        {
            _characterHealth.Changed += CharacterHealthOnChanged;
        }

        private void CharacterHealthOnChanged()
        {
            if (_characterHealth.Value == 0)
                StartCoroutine(GameOverCoroutine());
        }
        
        private IEnumerator GameOverCoroutine()
        {
            _fadeInScreen.DOColor(Color.black, 1f);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}