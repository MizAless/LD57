using System.Collections;
using UnityEngine;

namespace _Game.Scripts
{
    public class PlantWormSpawner : MonoBehaviour
    {
        [SerializeField] private WormPlant _wormPlantPrefab;
        [SerializeField] private Transform[] _spawnPoints;

        [SerializeField] private float _cooldown;

        private bool _isSpawning = false;

        public void StartSpawning()
        {
            StartCoroutine(Spawninig());
        }
        
        public void StopSpawning()
        {
            _isSpawning = false;
        }

        private IEnumerator Spawninig()
        {
            if (_isSpawning) 
                yield break;
            
            _isSpawning = true;

            while (_isSpawning)
            {
                var randomIndex = Random.Range(0, _spawnPoints.Length);

                var wormPlant = Instantiate(_wormPlantPrefab, _spawnPoints[randomIndex].position, Quaternion.identity);
                
                wormPlant.transform.SetParent(_spawnPoints[randomIndex].parent);
                
                if (randomIndex == 0)
                    wormPlant.transform.localScale = new Vector3(-1, 1, 1) * 0.3f;
                
                // wormPlant.transform.rotation = Quaternion.Euler(0, 0, 0);
                
                yield return new WaitForSeconds(_cooldown);
            }
        }
    }
}