using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class Scenario : MonoBehaviour
    {
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private float _gameTime = 2f * 15f;

        [SerializeField] private Transform _leftWall;
        [SerializeField] private Transform _rightWall;

        [SerializeField] private Mover _mover;

        [SerializeField] private Material[] _materials;

        [SerializeField] private Texture[] _stagesTextures;
        [SerializeField] private Color _startBGColor;

        [SerializeField] private ObstaclesSpawner _obstaclesSpawner;
        [SerializeField] private Obstacle _secondStageObstacle;

        [SerializeField] private PlantWormSpawner _plantWormSpawner;

        [SerializeField] private SpriteRenderer _flashLight;
        [SerializeField] private Worm _wormPrefab;
        [SerializeField] private Health _characterHealth;
        [SerializeField] private int _startStage = 1;

        [SerializeField] private Image _fadeInScreen;


        private int stage = 1;

        private void Start()
        {
            // PlayerPrefs.DeleteAll();

            stage = PlayerPrefs.GetInt("Stage", _startStage);

            StartCoroutine(Go());
            StartCoroutine(WallsMove());
            StartCoroutine(SettingMoverRange());
            StartCoroutine(SettingSpawnerRange());
            _obstaclesSpawner.StartSpawning();

            if (stage == 1 || stage == 2)
            {
                _materials[0].SetColor("_Color", _startBGColor);
                _materials[1].SetColor("_Color", Color.white);

                foreach (var material in _materials)
                {
                    material.SetFloat("_LerpFactor", 0);
                    material.SetTexture("_FromTex", _stagesTextures[stage - 1]);
                    material.SetTexture("_ToTex", _stagesTextures[stage - 1]);
                }
            }
            else
            {
                foreach (var material in _materials)
                {
                    material.SetFloat("_LerpFactor", 0);
                    material.SetTexture("_FromTex", _stagesTextures[1]);
                    material.SetTexture("_ToTex", _stagesTextures[1]);
                }
            }

            if (stage == 2)
            {
                Stage2();
            }

            if (stage == 3)
            {
                Stage3();
            }

            if (stage == 4)
            {
                Stage4();
            }
        }

        private IEnumerator Go()
        {
            var elapsedTime = (stage - 1) * _gameTime * 0.25f;

            while (elapsedTime < _gameTime)
            {
                elapsedTime += Time.deltaTime;
                _progressSlider.value = elapsedTime / _gameTime;
                yield return null;

                if (stage == 1 && _progressSlider.value > 0.25f)
                {
                    stage++;
                    PlayerPrefs.SetInt("Stage", stage);

                    Stage2();
                }

                if (stage == 2 && _progressSlider.value > 0.5f)
                {
                    stage++;
                    PlayerPrefs.SetInt("Stage", stage);

                    Stage3();
                }

                if (stage == 3 && _progressSlider.value > 0.75f)
                {
                    stage++;
                    PlayerPrefs.SetInt("Stage", stage);

                    Stage4();
                }
            }
            
            _obstaclesSpawner.StopSpawning();
            _plantWormSpawner.StopSpawning();
            
            yield return new WaitForSeconds(1f);

            _characterHealth.CantHit();
            Instantiate(_wormPrefab);
            _fadeInScreen.DOColor(Color.black, 1.5f);
            yield return new WaitForSeconds(0.5f);
            SoundManager.Instance.PlayWormEatSound();
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("EndScene");
        }

        private void Stage2()
        {
            SoundManager.Instance.PlayMonsterSound();
            StartCoroutine(ChangeBiom(_stagesTextures[stage - 1]));
            StartCoroutine(SetDarkening(_gameTime * 0.25f));
            _obstaclesSpawner.SetPrefab(_secondStageObstacle);
        }

        private void Stage3()
        {
            TurnOnFlashlight();
            _obstaclesSpawner.StopSpawning();
            _plantWormSpawner.StartSpawning();
            _obstaclesSpawner.SetPrefab(_secondStageObstacle);
        }

        private void Stage4()
        {
            TurnOffFlashlight();
            _obstaclesSpawner.SetCooldownRange(0);
            _obstaclesSpawner.SetCooldown(2f);
            _obstaclesSpawner.SetSizeMultiplier(0.5f);
            _obstaclesSpawner.StartSpawning();
            _plantWormSpawner.StartSpawning();
            _obstaclesSpawner.SetPrefab(_secondStageObstacle);
        }

        private IEnumerator SettingMoverRange()
        {
            while (_progressSlider.value < 1f)
            {
                _mover.Range = Mathf.Lerp(_mover.StartRange, _mover.EndRange, _progressSlider.value);
                yield return null;
            }
        }

        private IEnumerator SettingSpawnerRange()
        {
            while (_progressSlider.value < 1f)
            {
                _obstaclesSpawner.Range = Mathf.Lerp(_obstaclesSpawner.StartRange, _obstaclesSpawner.EndRange,
                    _progressSlider.value);
                yield return null;
            }
        }

        private IEnumerator WallsMove()
        {
            while (_progressSlider.value < 1f)
            {
                var rightPositionX = Mathf.Lerp(15, 10, _progressSlider.value);
                _rightWall.transform.position = _rightWall.transform.position.WithX(rightPositionX);
                var leftPositionX = Mathf.Lerp(-15, -10, _progressSlider.value);
                _leftWall.transform.position = _leftWall.transform.position.WithX(leftPositionX);
                yield return null;
            }
        }

        private void TurnOffFlashlight()
        {
            _flashLight.DOColor(Color.white.WithAlpha(0f), 1f)
                .SetEase(Ease.InBounce)
                .OnComplete(() => _flashLight.gameObject.SetActive(false));
        }

        private void TurnOnFlashlight()
        {
            _flashLight.gameObject.SetActive(true);

            var targetColor = _flashLight.color;
            _flashLight.color = Color.white.WithAlpha(0f);
            _flashLight.DOColor(targetColor, 1f).SetEase(Ease.InBounce);
        }

        private IEnumerator ChangeBiom(Texture texture)
        {
            foreach (var material in _materials)
            {
                material.SetFloat("_LerpFactor", 0);
                material.SetTexture("_FromTex", material.GetTexture("_ToTex"));
                material.SetTexture("_ToTex", texture);
            }

            var elapsedTime = 0f;

            while (elapsedTime < 2f)
            {
                elapsedTime += Time.deltaTime;

                var lerpValue = elapsedTime / 2f;
                lerpValue = Mathf.Clamp01(lerpValue);

                foreach (var material in _materials)
                    material.SetFloat("_LerpFactor", lerpValue);

                yield return null;
            }
        }

        private IEnumerator SetDarkening(float duration)
        {
            Color[] startColors = new Color[_materials.Length];
            Color[] targetColors = new Color[_materials.Length];

            for (var i = 0; i < _materials.Length; i++)
            {
                startColors[i] = _materials[i].GetColor("_Color");
                // targetColors[i] = startColors[i] * 0.5f; // он должен стать темнее на 50%;

                var multiplier = 0.15f;

                targetColors[i] = new Color(
                    startColors[i].r * multiplier,
                    startColors[i].g * multiplier,
                    startColors[i].b * multiplier,
                    startColors[i].a
                );
            }

            var elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                for (var i = 0; i < _materials.Length; i++)
                {
                    _materials[i].SetColor("_Color",
                        Color.Lerp(startColors[i], targetColors[i], elapsedTime / (_gameTime * 0.25f)));
                }

                yield return null;
            }
        }
    }
}