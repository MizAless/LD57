using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts
{
    public class ObstaclesSpawner : MonoBehaviour
    {
        [SerializeField] private Obstacle _obstaclePrefab;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _cooldownRange = 1f;
        [SerializeField] private float _range = 3f;

        private void Start()
        {
            StartCoroutine(Spawning());
        }

        private IEnumerator Spawning()
        {
            while (enabled)
            {
                var obstacle = Instantiate(_obstaclePrefab, transform.position, Quaternion.identity);
                var randomX = Random.Range(-_range, _range);
                obstacle.transform.position = obstacle.transform.position.WithX(randomX);

                var cooldown = _cooldown + Random.Range(-_cooldownRange, _cooldownRange);
                
                yield return new WaitForSeconds(cooldown);
            }
        }
    }
}