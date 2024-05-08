using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public float fadeSpeed = 0.3f;
    private Image fadeImage;

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(0));
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(1));
    }

    private IEnumerator Fade(float targetOpacity)
    {
        Color color = fadeImage.color;

        while (Mathf.Abs(color.a - targetOpacity) > 0.01f)
        {
            color.a = Mathf.MoveTowards(color.a, targetOpacity, fadeSpeed * Time.deltaTime + 0.01f);
            fadeImage.color = color;
            yield return null;
        }
    }
}
