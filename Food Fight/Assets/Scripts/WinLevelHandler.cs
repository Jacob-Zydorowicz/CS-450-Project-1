/*
 * (Jacob Welch)
 * (WinLevelHandler)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLevelHandler : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject winScreen;
    #endregion

    #region Functions
    public void WinLevel(string nextLevel)
    {
        winScreen.SetActive(true);
        StartCoroutine(WaitToLoadLevel(nextLevel));
    }

    private IEnumerator WaitToLoadLevel(string nextLevel)
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(nextLevel);
    }
    #endregion
}
