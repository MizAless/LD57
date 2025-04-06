using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;

        [field: SerializeField] public float StartRange { get; private set; } = 6f;
        [field: SerializeField] public float EndRange { get; private set; } = 2f;

        [HideInInspector] public float Range;

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
            var clampedX = Mathf.Clamp(_transform.position.x, -Range, Range);
            _transform.position = transform.position.WithX(clampedX);
        }
    }
}