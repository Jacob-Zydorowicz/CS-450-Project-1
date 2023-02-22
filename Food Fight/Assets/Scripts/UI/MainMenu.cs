using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadScene(string temp)
    {
        SceneManager.LoadScene(temp);
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }


}
