using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class WormPlantHead : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Health health))
            {
                if (!health.CanHit)
                    return;
                
                health.TakeDamage(1);
                HitEffect.Instance.Execute();
                SoundManager.Instance.PlayHitSound();
            }
        }
    }
}