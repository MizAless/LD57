using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Game.Scripts
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _rocksAudioSource;
        [SerializeField] private AudioSource _hitAudioSource;
        [SerializeField] private AudioSource _monsterAudioSource;
        [SerializeField] private AudioSource _wormPlantHitAudioSource;
        [SerializeField] private AudioSource _wormEatAudioSource;
        
        [SerializeField] private AudioClip[] _wormPlantHitSounds;
        
        public static SoundManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void PlayRocksSound()
        {
            _rocksAudioSource.Play();
        }
        
        public void PlayHitSound()
        {
            _hitAudioSource.Play();
        }
        
        public void PlayMonsterSound()
        {
            _monsterAudioSource.Play();
        }
        
        public void PlayWormPlantSound()
        {
            _wormPlantHitAudioSource.clip = _wormPlantHitSounds[Random.Range(0, _wormPlantHitSounds.Length)];
            _wormPlantHitAudioSource.Play();
        }
        
        public void PlayWormEatSound()
        {
            _wormEatAudioSource.Play();
        }
    }
}