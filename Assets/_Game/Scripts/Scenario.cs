using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
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

        [SerializeField] private SpriteRenderer _flashLight;
        [SerializeField] private Worm _wormPrefab;

        private int stage = 1;

        private void Start()
        {
            _materials[0].SetColor("_Color", _startBGColor);
            _materials[1].SetColor("_Color", Color.white);

            foreach (var material in _materials)
            {
                material.SetFloat("_LerpFactor", 0);
                material.SetTexture("_FromTex", _stagesTextures[stage - 1]);
                material.SetTexture("_ToTex", _stagesTextures[stage - 1]);
            }

            StartCoroutine(Go());
            StartCoroutine(WallsMove());
            StartCoroutine(SettingMoverRange());
        }

        private IEnumerator Go()
        {
            var elapsedTime = 0f;

            while (elapsedTime < _gameTime)
            {
                elapsedTime += Time.deltaTime;
                _progressSlider.value = elapsedTime / _gameTime;
                yield return null;

                if (stage == 1 && _progressSlider.value > 0.25f)
                {
                    stage++;
                    StartCoroutine(ChangeBiom(_stagesTextures[stage - 1]));
                    StartCoroutine(SetDarkening());
                }

                if (stage == 2 && _progressSlider.value > 0.5f)
                {
                    stage++;

                    TurnOnFlashlight();
                }

                if (stage == 3 && _progressSlider.value > 0.75f)
                {
                    stage++;

                    TurnOffFlashlight();
                }
            }

            Instantiate(_wormPrefab);
        }

        private IEnumerator SettingMoverRange()
        {
            while (_progressSlider.value < 1f)
            {
                _mover.Range = Mathf.Lerp(_mover.StartRange, _mover.EndRange , _progressSlider.value);
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

        private IEnumerator SetDarkening()
        {
            Color[] startColors = new Color[_materials.Length];
            Color[] targetColors = new Color[_materials.Length];

            for (var i = 0; i < _materials.Length; i++)
            {
                startColors[i] = _materials[i].GetColor("_Color");
                // targetColors[i] = startColors[i] * 0.5f; // он должен стать темнее на 50%;

                var multiplier = 0.25f;

                targetColors[i] = new Color(
                    startColors[i].r * multiplier,
                    startColors[i].g * multiplier,
                    startColors[i].b * multiplier,
                    startColors[i].a
                );
            }

            var elapsedTime = 0f;

            while (elapsedTime < _gameTime * 0.25f)
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