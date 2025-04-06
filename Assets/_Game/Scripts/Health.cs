using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _startHealth = 3;
        
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
            
            Changed?.Invoke();
            print(Value);
        }
    }
}