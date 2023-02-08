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

    [Range(0, 1000)]
    [Tooltip("The amount of ammo used per shot")]
    [SerializeField] protected int AmmoPerShot = 1;

    [Range(0.0f, 180.0f)]
    [Tooltip("The distance that the projectile can travel")]
    [SerializeField] protected float spread = 0.0f;
    #endregion
    string fire = "Fire";
    string firing = "Firing";
    private Animator an;
    private void Start()
    {
        an = transform.parent.parent.gameObject.GetComponent<Animator>();
    }

    #region Functions
    protected virtual void Awake()
    {
        weapon = transform.parent.gameObject.GetComponent<Weapon>();
    }

    public virtual void PerformAction()
    {
        an.SetBool(firing, true);
        an.SetTrigger(fire);
        weapon.UseAmmo(AmmoPerShot);
    }
    #endregion
}
