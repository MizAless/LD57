using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class HelloScript : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        StartCoroutine(Hello());
    }

    private IEnumerator Hello()
    {
        _text.text = "This game was made in 48 hours for Ludum Dare...";
        _text.DOFade(0, 2f);

        yield return new WaitForSeconds(2f);

        _text.color = Color.white;
        _text.text = "... by Mizaless";
        _text.DOFade(0f, 2f);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("MainScene");
    }
}