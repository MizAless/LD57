using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class HelloScript : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private Vector3 _startScale;

    private bool _done = false;

    private void Start()
    {
        _startScale = _text.transform.localScale;
        StartCoroutine(Hello());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            _done = true;
    }

    private IEnumerator Hello()
    {
        yield return SetText("This game was made in 48 hours for Ludum Dare...", 4f);
        yield return SetText("... by Mizaless", 3f);

        SceneManager.LoadScene("MainScene");
    }


    private IEnumerator SetText(string text, float fadeDuration)
    {
        float scaleOffDuration = 0.5f;
        
        _text.color = Color.white;
        _text.text = text;
        _text.transform.localScale = Vector3.zero;
        _text.transform.DOScale(_startScale, 0.2f);
        _text.DOFade(0f, fadeDuration);

        yield return SetTimer(fadeDuration - scaleOffDuration);
        // _text.DOKill();
        yield return null;
        _text.transform.DOScale(Vector3.zero, scaleOffDuration);
        yield return new WaitForSeconds(scaleOffDuration);
    }

    private IEnumerator SetTimer(float duration)
    {
        _done = false;
        
        float elapsedTime = 0f;

        while (!_done)
        {
            elapsedTime += Time.deltaTime;
            _done = elapsedTime > duration;

            yield return null;
        }
    }

    private IEnumerator HideTextWithDelay(float delay, float fadeDuration)
    {
        yield return new WaitForSeconds(delay - fadeDuration);
        _text.transform.DOScale(Vector3.zero, fadeDuration);
    }
}