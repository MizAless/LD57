using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using Range = UnityEngine.SocialPlatforms.Range;

namespace _Game.Scripts
{
    public class ObstaclesSpawner : MonoBehaviour
    {
        [SerializeField] private Obstacle _obstaclePrefab;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _cooldownRange = 1f;
        
        [field: SerializeField] public float StartRange = 3f;
        [field: SerializeField] public float EndRange = 1f;

        public float Range;

        private void Start()
        {
            Range = StartRange;
            StartCoroutine(Spawning());
        }
        
        public void SetPrefab(Obstacle obstacle)
        {
            _obstaclePrefab = obstacle;
        }

        private IEnumerator Spawning()
        {
            while (enabled)
            {
                var obstacle = Instantiate(_obstaclePrefab, transform.position, Quaternion.identity);
                var randomX = Random.Range(-Range, Range);
                obstacle.transform.position = obstacle.transform.position.WithX(randomX);

                var cooldown = _cooldown + Random.Range(-_cooldownRange, _cooldownRange);
                
                yield return new WaitForSeconds(cooldown);
            }
        }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.green;
        //     Gizmos.DrawCube(transform.position, new Vector3(Range * 2, 10, 1));
        // }
    }
}