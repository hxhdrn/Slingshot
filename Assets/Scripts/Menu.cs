using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public bool paused = false;

    [SerializeField] Canvas pauseMenu;

    public void PauseGame()
    {
        paused = !paused;
        Time.timeScale = paused ? 0f : 1f;
        pauseMenu.enabled = paused;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
