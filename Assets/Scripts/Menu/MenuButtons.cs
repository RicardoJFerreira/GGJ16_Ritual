using UnityEngine;
using System.Collections;

public class MenuButtons : MonoBehaviour {

    public void StartScene()
    {
        Application.LoadLevel("level_1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
