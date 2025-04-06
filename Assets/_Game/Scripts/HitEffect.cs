using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts
{
    public class HitEffect : MonoBehaviour
    {
        [SerializeField] private Transform[] _transforms;
        [SerializeField] private float _duration;
        [SerializeField] private float _strength;
        
        public static HitEffect Instance { get; private set; }

        private void Start()
        {
            Instance = this;
        }
        
        public void Execute()
        {
            foreach (var transform1 in _transforms)
            {
                transform1.DOShakeScale(_duration, _strength);
                // transform1.DOShakePosition(_duration, _strength);
            }
        }
    }
}