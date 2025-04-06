using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _range = 5f;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            // float vertical = Input.GetAxis("Vertical");

            _transform.position += Vector3.right * (horizontal * Time.deltaTime * _speed);
            var clampedX = Mathf.Clamp(_transform.position.x, -_range, _range);
            _transform.position = transform.position.WithX(clampedX);
        }
    }
}