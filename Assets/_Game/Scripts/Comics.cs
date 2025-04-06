using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Scripts
{
    public class Comics : MonoBehaviour
    {
        [SerializeField] private Transform Slide1;
        [SerializeField] private Transform Slide2;
        [SerializeField] private Transform Slide3;
        [SerializeField] private Transform Slide4;
        
        [SerializeField] private AudioSource _fallingSound;
        
        private Vector3 _slide1TargetPosition;
        private Vector3 _slide2TargetPosition;
        private Vector3 _slide3TargetPosition;
        private Vector3 _slide4TargetPosition;

        private float _offset = 1500f;
        
        private bool _done;

        private void Start()
        {
            _slide1TargetPosition = Slide1.position;
            _slide2TargetPosition = Slide2.position;
            _slide3TargetPosition = Slide3.position;
            _slide4TargetPosition = Slide4.position;
            
            Slide1.position = Slide1.position.WithX(Slide1.position.x - _offset);
            Slide2.position = Slide2.position.WithY(Slide2.position.y - _offset);
            Slide3.position = Slide3.position.WithX(Slide3.position.x + _offset);
            Slide4.position = Slide4.position.WithX(Slide4.position.x + _offset);
            
            StartCoroutine(Show());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                _done = true;
        }

        private IEnumerator Show()
        {
            Slide1.DOMove(_slide1TargetPosition, 0.3f).SetEase(Ease.InQuad);
            
            yield return SetTimer(3f);
            
            Slide2.DOMove(_slide2TargetPosition, 0.3f).SetEase(Ease.InQuad);
            
            yield return SetTimer(5f);
            
            Slide3.DOMove(_slide3TargetPosition, 0.3f).SetEase(Ease.InQuad);
            
            yield return SetTimer(3f);
            
            Slide4.DOMove(_slide4TargetPosition, 0.3f).SetEase(Ease.InQuad);
            _fallingSound.Play();
            
            yield return SetTimer(2.5f);
            yield return SetTimer(0.5f);
            
            SceneManager.LoadScene("MainScene");
        }
        
        private IEnumerator SetTimer(float duration)
        {
            _done = false;
        
            float elapsedTime = 0f;

            while (!_done)
            {
                elapsedTime += Time.deltaTime;
                _done = elapsedTime > duration;

                yield return null;
            }
        }
    }
}