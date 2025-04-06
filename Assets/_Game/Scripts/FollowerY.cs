using UnityEngine;

namespace _Game.Scripts
{
    public class FollowerY : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void Update()
        {
            _transform.position = _transform.position.WithY(_target.position.y);
        }
    }
}