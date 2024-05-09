using System.Collections;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject mainCharacter;
    public GameObject ghostCharacter;
    public Camera mainCamera;
    public Camera ghostCamera;
    public ScreenFader screenFader;

    void Start()
    {
        ghostCamera.enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Switch"))
        {
            if (mainCharacter.activeSelf)
            {
                StartCoroutine(SwitchToGhost());
            }
            else
            {
                StartCoroutine(SwitchToMain());
            }
        }
    }

    IEnumerator SwitchToMain()
    {
        screenFader.FadeIn();
        yield return new WaitForSeconds(screenFader.fadeSpeed);
        mainCharacter.SetActive(true);
        ghostCharacter.SetActive(false);
        mainCamera.enabled = true;
        ghostCamera.enabled = false;
        screenFader.FadeOut();
    }

    IEnumerator SwitchToGhost()
    {
        screenFader.FadeIn();
        yield return new WaitForSeconds(screenFader.fadeSpeed);
        mainCharacter.SetActive(false);
        ghostCharacter.SetActive(true);
        mainCamera.enabled = false;
        ghostCamera.enabled = true;
        screenFader.FadeOut();
    }
}
