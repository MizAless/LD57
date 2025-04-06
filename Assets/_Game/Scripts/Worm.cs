using UnityEngine;

namespace _Game.Scripts
{
    public class Worm : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        
        private void Update()
        {
            transform.position += Vector3.up * Time.deltaTime * _speed; 
        }
    }
}