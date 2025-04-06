using UnityEngine;

namespace _Game.Scripts
{
    public class DestructionZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(other.gameObject);
        }
    }
}