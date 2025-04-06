using UnityEngine;

namespace _Game.Scripts
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        
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
                health.TakeDamage(1);
                Destroy(gameObject);
            }
        }
    }
}