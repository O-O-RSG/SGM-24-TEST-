using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Return - это код клавиши Enter
        {
            StartGame();
        }
        if (Input.GetKeyDown(KeyCode.Space)) // Return - это код клавиши Enter
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}