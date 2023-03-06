/*
 * (Jacob Welch)
 * (DeathScreenHandler)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenHandler : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject deathScreen;
    #endregion

    #region Functions
    /// <summary>
    /// Handles initilization of components and other fields before anything else.
    /// </summary>
    private void Awake()
    {
        FindObjectOfType<PlayerHealthController>().DeathEvent.AddListener(PlayerDeath);
    }

    private void PlayerDeath()
    {
        deathScreen.SetActive(true);
        StartCoroutine(ReloadScene());
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
}
