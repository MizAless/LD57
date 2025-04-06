using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class EndScene : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private AudioSource _transmitterAudioSource;

        private void Start()
        {
            PlayerPrefs.DeleteAll();
            StartCoroutine(Go());
        }
        
        private IEnumerator Go()
        {
            var targetPosition = _image.transform.position;
            _image.transform.position = _image.transform.position.WithY(_image.transform.position.y + 2000f);
            _image.transform.DOMove(targetPosition, 1f);
            yield return new WaitForSeconds(1f);
            
            _transmitterAudioSource.Play();
            
            yield return new WaitForSeconds(3f);
            _image.transform.DOMove(_image.transform.position.WithY(_image.transform.position.y - 2000f), 1f);
        }
    }
}