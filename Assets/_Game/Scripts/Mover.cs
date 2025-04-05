using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _range;
        
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            // float vertical = Input.GetAxis("Vertical");
            
            _transform.position += Vector3.right * horizontal * Time.deltaTime * _speed;
            _transform.position += Vector3.up * horizontal * Time.deltaTime * _speed;
        }
    }
}