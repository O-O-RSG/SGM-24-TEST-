using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Return - это код клавиши Enter
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("PlayMenu");
    }
}