using UnityEngine;

namespace _Game.Scripts
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private ParticleSystem _effectPrefab;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            _transform.position += _transform.up * (_speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Health health))
            {
                if (!health.CanHit)
                    return;
                
                health.TakeDamage(1);
                var effect = Instantiate(_effectPrefab, transform.position, _effectPrefab.transform.rotation);
                effect.Play();
                HitEffect.Instance.Execute();
                SoundManager.Instance.PlayRocksSound();
                SoundManager.Instance.PlayHitSound();
                Destroy(gameObject);
            }
        }
    }
}