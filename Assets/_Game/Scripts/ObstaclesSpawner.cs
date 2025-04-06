using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts
{
    public class ObstaclesSpawner : MonoBehaviour
    {
        [SerializeField] private Obstacle _obstaclePrefab;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _cooldownRange = 1f;
        
        [field: SerializeField] public float StartRange = 3f;
        [field: SerializeField] public float EndRange = 1f;

        [HideInInspector] public float Range;

        private bool _isSpawning = false;
        
        private float _sizeMultiplier = 1f;
        
        private void Start()
        {
            Range = StartRange;
        }
        
        public void StartSpawning()
        {
            StartCoroutine(Spawning());
        }
        
        public void StopSpawning()
        {
            _isSpawning = false;
        }
        
        public void SetSizeMultiplier(float value)
        {
            _sizeMultiplier = value;
        }

        public void SetCooldownRange(float range)
        {
            _cooldownRange = range;
        }
        
        public void SetCooldown(float cooldown)
        {
            _cooldown = cooldown;
        }
        
        public void SetPrefab(Obstacle obstacle)
        {
            _obstaclePrefab = obstacle;
        }

        private IEnumerator Spawning()
        {
            if (_isSpawning)
                yield break;
            
            _isSpawning = true;
            
            while (_isSpawning)
            {
                var cooldown = _cooldown + Random.Range(-_cooldownRange, _cooldownRange);
                
                yield return new WaitForSeconds(cooldown);

                var obstacle = Instantiate(_obstaclePrefab, transform.position, Quaternion.identity);
                var randomX = Random.Range(-Range, Range);
                obstacle.transform.position = obstacle.transform.position.WithX(randomX);
                obstacle.transform.localScale *= _sizeMultiplier;
            }
        }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.green;
        //     Gizmos.DrawCube(transform.position, new Vector3(Range * 2, 10, 1));
        // }
    }
}