using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public bool isPaused = false; // On pause or not

    void Start()
    {
     
    }


    void Update()
    {
        // No longer paused
        if (Input.GetButtonDown("Cancel"))
        {
            isPaused = !isPaused;
        }
        TogglePauseMenu();
    }

    public void TogglePauseMenu()
    {
        // not the optimal way but for the sake of readability
        if (!isPaused)
        {
            gameObject.GetComponentInChildren<Canvas>().enabled = false;
            Time.timeScale = 1.0f;
        }
        else
        {
            gameObject.GetComponentInChildren<Canvas>().enabled = true;
            Time.timeScale = 0f;
        }
    }


// Methods for onClick()

public void StartScene()
    {
        Application.LoadLevel("level_1");
    }

    public void ContinueScene()
    {
        isPaused = false;
       // TogglePauseMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
