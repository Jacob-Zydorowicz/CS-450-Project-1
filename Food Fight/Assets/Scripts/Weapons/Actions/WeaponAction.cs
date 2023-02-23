/*
 * (Jacob Welch)
 * (WeaponAction)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponAction : MonoBehaviour
{
    #region Fields
    protected Weapon weapon;

    [field: Range(0.0f, 10000.0f)]
    [field: Tooltip("The distance that the projectile can travel")]
    [field: SerializeField] public float Range { get; private set; } = 10.0f;

    [field: Range(0, 10000)]
    [field: Tooltip("The damage that this weapon action deals")]
    [field: SerializeField] public int Damage { get; private set; } = 5;

    [Range(0, 1000)]
    [Tooltip("The amount of ammo used per shot")]
    [SerializeField] protected int AmmoPerShot = 1;

    [Range(0.0f, 180.0f)]
    [Tooltip("The distance that the projectile can travel")]
    [SerializeField] protected float spread = 0.0f;

    [Tooltip("The sound played when throwing an attack")]
    [SerializeField] private AudioClip attackSound;

    [Range(0.0f, 1.0f)]
    [Tooltip("Attack sound volume")]
    [SerializeField] private float attackSoundVolume = 1.0f;

    #region Animation
    private string fire = "Fire";
    private string firing = "Firing";
    private Animator an;
    #endregion
    #endregion

    #region Functions
    protected virtual void Awake()
    {
        weapon = transform.parent.gameObject.GetComponent<Weapon>();
    }

    private void Start()
    {
        an = transform.parent.parent.gameObject.GetComponent<Animator>();
    }

    public virtual void PerformAction()
    {
        SoundManager.PlaySound(attackSound, attackSoundVolume, transform.position);
        an.SetBool(firing, true);
        an.SetTrigger(fire);
        weapon.UseAmmo(AmmoPerShot);
    }

    protected Quaternion Spread()
    {
        return Quaternion.Euler(new Vector3(0, 0, Random.Range(-spread, spread)));
    }
    #endregion
}
