/*
 * (Jacob Welch)
 * (Weapon)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    #region Fields
    private WeaponAction weaponAction;

    [field: Header("Ammo")]
    [field: Tooltip("Holds true if this weapon uses ammo")]
    [field: SerializeField] public bool UsesAmmo { get; private set; } = true;

    [field:Range(1, 1000)]
    [field:Tooltip("The amount of ammo the player has before needing to reload")]
    [field:SerializeField] public int ClipSize { get; private set; } = 5;

    [field: Range(0, 50)]
    [field:Tooltip("The amount of ammo used per shot")]
    [field:SerializeField] public int AmmoPerShot { get; private set; } = 1;

    [field: Range(0.0f, 5.0f)]
    [field:Tooltip("The length of time needed to reload this weapon")]
    [field:SerializeField] public float ReloadTime { get; private set; } = 1.0f;

    [field: Header("Actions")]
    [field:Range(0.0f, 5.0f)]
    [field:Tooltip("The minimum delay between shots being taken")]
    [field:SerializeField] public float TimeBetweenShots { get; private set; } = 0.5f;

    [field:Range(0.0f, 10000.0f)]
    [field:Tooltip("The distance that the projectile can travel")]
    [field:SerializeField] public float Range { get; private set; } = 10.0f;

    [Tooltip("The alternate action of this weapon (Leave empty if none)")]
    [SerializeField] private Weapon altActionWeapon;

    protected int currentAmmo;
    #endregion

    #region Functions
    /// <summary>
    /// Handles initilization of components and other fields before anything else.
    /// </summary>
    protected virtual void Awake()
    {
        currentAmmo = ClipSize;
        weaponAction = GetComponent<WeaponAction>();
    }

    public void Aim(Vector2 aimDir)
    {
        transform.right = aimDir;
    }

    public abstract void MainActionDown();
    public abstract void MainActionUp();

    public void AltActionDown()
    {
        if (altActionWeapon == null) return;

        altActionWeapon.MainActionDown();
    }

    public void AltActionUp()
    {
        if (altActionWeapon == null) return;

        altActionWeapon.MainActionUp();
    }

    protected virtual void PerformWeaponAction()
    {
        weaponAction.PerformAction();

        if (UsesAmmo) currentAmmo -= AmmoPerShot;

        if(currentAmmo == 0) StartCoroutine(Reload());
    }

    protected virtual IEnumerator Reload()
    {
        yield return new WaitForSeconds(ReloadTime);
        currentAmmo = ClipSize;
    }

    protected bool HasAmmo()
    {
        return currentAmmo > 0;
    }
    #endregion
}
