/*
 * (Jacob Welch)
 * (HealthBarHandler)
 * (Food Fight)
 * (Description: A script for handling the fill of health bars.)
 */
using UnityEngine;
using UnityEngine.UI;

public class HealthBarHandler : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// The image used for the health bar.
    /// </summary>
    private Image healthBarImage;
    #endregion

    #region Functions
    /// <summary>
    /// Initializes the components.
    /// </summary>
    private void Awake()
    {
        healthBarImage = GetComponent<Image>();
    }

    /// <summary>
    /// Updates the fill of the health bar.
    /// </summary>
    /// <param name="currentAmount">The current value of the bar.</param>
    /// <param name="maxAmount">The max value of the bar.</param>
    public void UpdateFill(int currentAmount, int maxAmount)
    {
        if (healthBarImage == null) return;

        healthBarImage.fillAmount = (float)currentAmount / maxAmount;
    }
    #endregion
}
