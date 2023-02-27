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
    #region Health
    [Tooltip("The maximum health that this object can have")]
    [SerializeField] protected int maxHealth = 100;

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

    public UnityEvent DeathEvent = new UnityEvent();

    #region Destruction Sounds
    [Tooltip("The sounds that can be made when this object dies")]
    [SerializeField] private AudioClip[] destrucitonSounds = new AudioClip[0];

    [Range(0.0f, 1.0f)]
    [Tooltip("The volume of the destruction sounds")]
    [SerializeField] private float destructionSoundVolume = 1.0f;
    #endregion
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
            PlayDestructionSounds();
            DestructionEvent();
        }
    }

    private void PlayDestructionSounds()
    {
        if (destrucitonSounds.Length == 0) return;

        SoundManager.PlaySound(destrucitonSounds[Random.Range(0, destrucitonSounds.Length)], destructionSoundVolume, transform.position);
    }

    protected virtual void DestructionEvent()
    {
        DeathEvent.Invoke();
        Destroy(gameObject);
    }
    #endregion
}
