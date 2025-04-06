using UnityEngine;

namespace _Game.Scripts
{
    public class Character : MonoBehaviour
    {
        public static Character Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}