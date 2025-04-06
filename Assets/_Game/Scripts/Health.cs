using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _startHealth = 3;
        [SerializeField] private float _invulnerabilityDuration = 2f;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        public bool CanHit { get; private set; } = true;
        public int Value { get; private set; }

        public event Action Changed;

        private void Start()
        {
            Value = _startHealth;
        }

        public void TakeDamage(int damage)
        {
            Value -= damage;

            if (Value <= 0)
            {
                Value = 0;
            }

            StartCoroutine(Invulnerability());
            Changed?.Invoke();
            print(Value);
        }

        private IEnumerator Invulnerability()
        {
            CanHit = false;

            float elapsedTime = 0f;
            float blinkInterval = 0.1f;
            bool isVisible = true;

            while (elapsedTime < _invulnerabilityDuration - 0.5f)
            {
                isVisible = !isVisible;
                _spriteRenderer.enabled = isVisible;

                elapsedTime += blinkInterval;
                yield return new WaitForSeconds(blinkInterval);
            }

            _spriteRenderer.enabled = true;

            yield return new WaitForSeconds(0.5f);
            CanHit = true;
        }
    }
}