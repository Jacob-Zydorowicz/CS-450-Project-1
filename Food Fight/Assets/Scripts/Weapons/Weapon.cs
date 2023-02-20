/*
 * (Jacob Welch)
 * (Weapon)
 * (Food Fight)
 * (Description: A base class for all weapons.)
 */
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    #region Fields
    private WeaponRoutine mainWeaponRoutine;
    private WeaponRoutine altWeaponRoutine;

    // TODO
    #region Ammo
    [Header("Ammo")]
    [Range(0, 1000)]
    [Tooltip("The amount of ammo the player has before needing to reload (setting it to 0 means this weapon does not use ammo/has infinite)")]
    [SerializeField] private int clipSize = 5;

    [Range(0.0f, 5.0f)]
    [Tooltip("The length of time needed to reload this weapon")]
    [SerializeField] private float reloadTime = 1.0f;

    /// <summary>
    /// An event thats sends the current ammo of the weapon.
    /// </summary>
    private UnityEvent<int> UpdateAmmoLeft = new UnityEvent<int>();

    /// <summary>
    /// The durrent amount of ammo this weapon has.
    /// </summary>
    private int currentAmmo = 0;

    /// <summary>
    /// The current amount of ammo this weapon has.
    /// </summary>
    protected int CurrentAmmo 
    { 
        get=>currentAmmo;  
        private set
        {
            currentAmmo = value;
            currentAmmo = Mathf.Clamp(currentAmmo, 0, clipSize);
            UpdateAmmoLeft.Invoke(currentAmmo);
        }
    }
    #endregion

    #region Actions
    [Header("Actions")]
    [Range(0.0f, 5.0f)]
    [Tooltip("The time it takes to swap to this weapon")]
    [SerializeField] protected float swapToTime = 0.6f;

    /// <summary>
    /// Holds true if this weapon can currently be shot.
    /// </summary>
    public bool canShoot { get; private set; } = true;

    [HideInInspector] public float nextShotAllowedTime = -Mathf.Infinity;
    #endregion
    #endregion

    #region Functions
    /// <summary>
    /// Handles initilization of components and other fields before anything else.
    /// </summary>
    protected virtual void Awake()
    {
        CurrentAmmo = clipSize;

        #region Set Weapon Routines
        var weaponRoutines = GetComponentsInChildren<WeaponRoutine>();

        if(weaponRoutines.Length > 0)
        {
            mainWeaponRoutine = weaponRoutines[0];

            if(weaponRoutines.Length > 1)
            {
                altWeaponRoutine = weaponRoutines[1];
            }
        }
        #endregion
    }

    protected virtual void Start()
    {
        if (GameSubject.sceneInstance != null) UpdateAmmoLeft.AddListener(GameSubject.sceneInstance.UpdateAmmo);
    }

    #region Aim
    /// <summary>
    /// Sets the current aim direction of the weapon.
    /// </summary>
    /// <param name="aimDir">The direction to aim at.</param>
    public void Aim(Vector2 aimDir)
    {
        transform.right = aimDir;
    }
    #endregion

    #region Actions
    #region Main Alt
    #region Main Action
    /// <summary>
    /// Calls for an action to be taken when pression the main action button down.
    /// </summary>
    public virtual void MainActionDown()
    {
        if (mainWeaponRoutine == null) return;

        mainWeaponRoutine.WeaponDown();
    }

    /// <summary>
    /// Calls for an action to be taken when releasing the main action button.
    /// </summary>
    public virtual void MainActionUp()
    {
        if (mainWeaponRoutine == null) return;

        mainWeaponRoutine.WeaponUp();
    }
    #endregion

    #region Alt Action
    /// <summary>
    /// Calls for an action to be taken when pression the alt action button down.
    /// </summary>
    public virtual void AltActionDown()
    {
        if (altWeaponRoutine == null) return;

        altWeaponRoutine.WeaponDown();
    }

    /// <summary>
    /// Calls for an action to be taken when releasing the alt action button.
    /// </summary>
    public virtual void AltActionUp()
    {
        if (altWeaponRoutine == null) return;

        altWeaponRoutine.WeaponUp();
    }
    #endregion
    #endregion

    #region Swapping
    public void SwapToFrom(bool swapTo)
    {
        if (swapTo)
        {
            SwapTo();
        }
        else
        {
            SwapFrom();
        }
    }

    public void SwapTo()
    {
        SwapEventUpdates();
        StartCoroutine(SwapRoutine());
    }

    private void SwapEventUpdates()
    {
        if (GameSubject.sceneInstance != null) GameSubject.sceneInstance.UpdateWeapon(gameObject.name);
        UpdateAmmoLeft.Invoke(currentAmmo);
    }

    private IEnumerator SwapRoutine()
    {
        yield return new WaitForSeconds(swapToTime);
        canShoot = true;
    }

    public void SwapFrom()
    {
        StopAllCoroutines();
        canShoot = false;

        if (mainWeaponRoutine != null) mainWeaponRoutine.SwapOff();
        if (altWeaponRoutine != null) altWeaponRoutine.SwapOff();
    }
    #endregion

    #region Ammo
    /// <summary>
    /// Uses ammo of the weapon if this weapon uses ammo.
    /// </summary>
    public void UseAmmo(int ammoToUse)
    {
        if (clipSize != 0)
        {
            CurrentAmmo -= ammoToUse;
            

            if (CurrentAmmo == 0) StartCoroutine(Reload());
        }
    }

    /// <summary>
    /// Reloads the weapon when it runs out of ammo.
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator Reload()
    {
        canShoot = false;

        #region Reload Weapon Routine
        if (mainWeaponRoutine != null)
        mainWeaponRoutine.Reload();

        if(altWeaponRoutine != null)
        altWeaponRoutine.Reload();
        #endregion

        yield return new WaitForSeconds(reloadTime);
        CurrentAmmo = clipSize;
        canShoot = true;
    }
    #endregion
    #endregion
    #endregion
}
