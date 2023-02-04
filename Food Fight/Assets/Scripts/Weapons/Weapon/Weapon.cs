/*
 * (Jacob Welch)
 * (Weapon)
 * (Food Fight)
 * (Description: A base class for all weapons.)
 */
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    #region Fields
    private WeaponAction weaponAction;

    #region Ammo
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

    /// <summary>
    /// An event thats sends the current ammo of the weapon.
    /// </summary>
    private UnityEvent<int> UpdateAmmoLeft = new UnityEvent<int>();

    /// <summary>
    /// The current amount of ammo this weapon has.
    /// </summary>
    protected int currentAmmo;
    #endregion

    #region Actions
    [field: Header("Actions")]
    [field:Range(0.0f, 5.0f)]
    [field:Tooltip("The minimum delay between shots being taken")]
    [field:SerializeField] public float TimeBetweenShots { get; private set; } = 0.5f;

    [field:Range(0.0f, 10000.0f)]
    [field:Tooltip("The distance that the projectile can travel")]
    [field:SerializeField] public float Range { get; private set; } = 10.0f;

    [Tooltip("The alternate action of this weapon (Leave empty if none)")]
    [SerializeField] private Weapon altActionWeapon;
    #endregion
    #endregion

    #region Functions
    /// <summary>
    /// Handles initilization of components and other fields before anything else.
    /// </summary>
    protected virtual void Awake()
    {
        currentAmmo = ClipSize;
        weaponAction = GetComponent<WeaponAction>();

        if(altActionWeapon != null) UpdateAmmoLeft.AddListener(altActionWeapon.SetAmmo);
    }

    /// <summary>
    /// Sets the current aim direction of the weapon.
    /// </summary>
    /// <param name="aimDir">The direction to aim at.</param>
    public void Aim(Vector2 aimDir)
    {
        transform.right = aimDir;
    }

    #region Main Action
    /// <summary>
    /// Calls for an action to be taken when pression the main action button down.
    /// </summary>
    public abstract void MainActionDown();

    /// <summary>
    /// Calls for an action to be taken when releasing the main action button.
    /// </summary>
    public abstract void MainActionUp();
    #endregion

    #region Alt Action
    /// <summary>
    /// Calls for an action to be taken when pression the alt action button down.
    /// </summary>
    public void AltActionDown()
    {
        if (altActionWeapon == null) return;

        altActionWeapon.MainActionDown();
    }

    /// <summary>
    /// Calls for an action to be taken when releasing the alt action button.
    /// </summary>
    public void AltActionUp()
    {
        if (altActionWeapon == null) return;

        altActionWeapon.MainActionUp();
    }
    #endregion

    /// <summary>
    /// Performs the action of the weapon.
    /// </summary>
    protected virtual void PerformWeaponAction()
    {
        weaponAction.PerformAction();
        UseAmmo();
    }

    #region Ammo
    /// <summary>
    /// Uses ammo of the weapon if this weapon uses ammo.
    /// </summary>
    private void UseAmmo()
    {
        if (UsesAmmo)
        {
            currentAmmo -= AmmoPerShot;
            UpdateAmmoLeft.Invoke(currentAmmo);

            if (currentAmmo == 0) StartCoroutine(Reload());
        }
    }

    /// <summary>
    /// Syncs the ammo between main and alt action weapons.
    /// </summary>
    /// <param name="newAmmo"></param>
    public void SetAmmo(int newAmmo)
    {
        currentAmmo = newAmmo;
    }

    /// <summary>
    /// Reloads the weapon when it runs out of ammo.
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator Reload()
    {
        yield return new WaitForSeconds(ReloadTime);
        currentAmmo = ClipSize;
        UpdateAmmoLeft.Invoke(currentAmmo);
    }

    /// <summary>
    /// Holds true if the weapon has any ammo in it's clip.
    /// </summary>
    /// <returns></returns>
    protected bool HasAmmo()
    {
        return currentAmmo > 0;
    }
    #endregion
    #endregion
}
