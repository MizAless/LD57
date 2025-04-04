using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class HelloScript : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private Vector3 _startScale;

    private void Start()
    {
        _startScale = _text.transform.localScale;
        StartCoroutine(Hello());
    }

    private IEnumerator Hello()
    {
        SetText("This game was made in 48 hours for Ludum Dare...", 3f);
        yield return new WaitForSeconds(3f);

        SetText("... by Mizaless", 2f);
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("MainScene");
    }

    private void SetText(string text, float fadeDuration)
    {
        _text.color = Color.white;
        _text.text = text;
        _text.transform.localScale = Vector3.zero;
        _text.transform.DOScale(_startScale, 0.2f);
        _text.DOFade(0f, fadeDuration);
        StartCoroutine(HideTextWithDelay(fadeDuration, 0.5f));
    }

    private IEnumerator HideTextWithDelay(float delay, float fadeDuration)
    {
        yield return new WaitForSeconds(delay - fadeDuration);
        _text.transform.DOScale(Vector3.zero, fadeDuration);
    }
}