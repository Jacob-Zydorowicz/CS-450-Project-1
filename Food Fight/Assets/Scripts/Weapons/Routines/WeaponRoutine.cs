/*
 * (Jacob Welch)
 * (WeaponRoutine)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponRoutine : MonoBehaviour
{
    #region Fields
    // TODO
    #region Move To Routine
    [Range(0.0f, 5.0f)]
    [Tooltip("The minimum delay between shots being taken")]
    [SerializeField] protected float timeBetweenShots = 0.5f;
    #endregion

    protected bool hasInput = false;

    protected Weapon weapon;
    protected WeaponAction weaponAction;
    #endregion

    #region Functions
    protected virtual void Awake()
    {
        weapon = transform.parent.gameObject.GetComponent<Weapon>();
        weaponAction = GetComponent<WeaponAction>();
    }

    #region Inputs
    public virtual void WeaponDown()
    {
        hasInput = true;
    }

    public virtual void WeaponUp()
    {
        hasInput = false;
    }
    #endregion

    /// <summary>
    /// Performs the action of the weapon.
    /// </summary>
    protected virtual void PerformWeaponAction(float damageMod = 1.0f, float speedMod = 1.0f, float rangeMod = 1.0f)
    {
        weaponAction.PerformAction(damageMod, speedMod, rangeMod);
        weapon.nextShotAllowedTime = Time.time + timeBetweenShots;
    }

    public virtual void SwapOff()
    {
        hasInput = false;
        weapon.nextShotAllowedTime = -Mathf.Infinity;
        StopAllCoroutines();
    }

    public virtual void Reload()
    {

    }

    protected bool ShotTimeAllowed()
    {
        return Time.time > weapon.nextShotAllowedTime;
    }
    #endregion
}
