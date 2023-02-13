/*
 * (Jacob Welch)
 * (Damageable)
 * (Food Fight)
 * (Description: base scripts)
 */
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    #region Fields
    [Tooltip("The maximum health that this object can have")]
    [SerializeField] private int maxHealth = 100;

    /// <summary>
    /// An event that is called every time the health is modified.
    /// </summary>
    public UnityEvent<int, int> HealthChangeEvent = new UnityEvent<int, int>();

    /// <summary>
    /// The field for the current health of the damageable.
    /// </summary>
    private int currentHealth = 0;

    /// <summary>
    /// The property for getting and modifying the health of the demageable.
    /// </summary>
    protected int CurrentHealth
    {
        get=>currentHealth;
        set
        {
            currentHealth = value;
            HealthChangeEvent.Invoke(currentHealth, maxHealth);
        }
    }
    #endregion

    #region Functions
    /// <summary>
    /// Initializes the health of the damageable.
    /// </summary>
    protected virtual void Awake()
    {
        CurrentHealth = maxHealth;
    }

    /// <summary>
    /// Updates the current health of the damageable.
    /// </summary>
    /// <param name="healthMod">The modifier to be subtracted from the current health.</param>
    public void UpdateHealth(int healthMod)
    {
        if (currentHealth == 0) return;

        CurrentHealth -= healthMod;

        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

        if (currentHealth == 0)
        {
            DestructionEvent();
        }
    }

    protected virtual void DestructionEvent()
    {
        Destroy(gameObject);
    }
    #endregion
}
