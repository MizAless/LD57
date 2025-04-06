using System.Collections;
using UnityEngine;

namespace _Game.Scripts
{
    public class WormPlant : MonoBehaviour
    {
        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
        [SerializeField] private float _speed;
        [SerializeField] private Animator _animator;
        
        private Character _character;

        private void Start()
        {
            _character = Character.Instance;

            StartCoroutine(LifeCycle());
        }

        private void Update()
        {
            transform.localPosition += Vector3.up * (_speed * Time.deltaTime * transform.localScale.y);    
        }
        
        private IEnumerator LifeCycle()
        {
            var evaluateTime = 0f;

            while (evaluateTime < 2f)
            {
                evaluateTime += Time.deltaTime;
                
                Vector3 direction = _character.transform.position + Vector3.down * 3f  - transform.position;
                transform.up = direction;
                
                // transform.LookAt(_character.transform.position); замени на метод подходящий для 2d, то есть чтобы объект смотрел на персонажа своей осью Y 
                yield return null;
            }
            
            Attack();
        }
        
        private void Attack()
        {
            _animator.SetTrigger(IsAttacking);
            SoundManager.Instance.PlayWormPlantSound();
        }
    }
}