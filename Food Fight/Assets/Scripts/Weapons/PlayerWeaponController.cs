/*
 * (Jacob Welch)
 * (PlayerWeaponController)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    #region Fields
    [Tooltip("The weapons the player currently possesses")]
    [SerializeField] private Weapon[] weapons = new Weapon[2];
    private int equippedWeaponIndex = 0;

    private Vector2 aimDir;

    #region Input Keycode
    #region Equip
    [Tooltip("Equips the weapon in the first wepaon slot")]
    [SerializeField] private KeyCode equipWeapon1Keycode = KeyCode.Alpha1;

    [Tooltip("Equips the weapon in the second weapon slot")]
    [SerializeField] private KeyCode equipWeapon2Keycode = KeyCode.Alpha2;
    #endregion

    #region Weapon Actions
    [Tooltip("Uses the main action for the weapon")]
    [SerializeField] private KeyCode mainWeaponActionKeycode = KeyCode.Mouse0;

    [Tooltip("Uses the alt action for the weapon (Not all weapons have one)")]
    [SerializeField] private KeyCode altWeaponActionKeycode = KeyCode.Mouse1;
    #endregion
    #endregion
    #endregion

    #region Functions
    /// <summary>
    /// Handles initilization of components and other fields before anything else.
    /// </summary>
    private void Awake()
    {
        
    }

    /// <summary>
    /// Calls for an event to take place once per frame after camera ticks.
    /// </summary>
    private void LateUpdate()
    {
        WeaponInputs();
    }

    // TODO handle aiming stuff better
    #region Inputs
    private void WeaponInputs()
    {
        WeaponChangeInput();

        if (weapons.Length <= equippedWeaponIndex || weapons[equippedWeaponIndex] == null) return;

        AimWeaponInput();
        WeaponActionInput();
    }

    // TODO
    private void AimWeaponInput()
    {
        aimDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        AimWeapon();
    }

    #region Weapon Action
    private void WeaponActionInput()
    {
        #region Main
        if (Input.GetKeyDown(mainWeaponActionKeycode))
        {
            MainWeaponActionDown();
        }

        if (Input.GetKeyUp(mainWeaponActionKeycode))
        {
            MainWeaponActionUp();
        }
        #endregion

        #region Alt
        if (Input.GetKeyDown(altWeaponActionKeycode))
        {
            AltWeaponActionDown();
        }

        if (Input.GetKeyUp(altWeaponActionKeycode))
        {
            AltWeaponActionUp();
        }
        #endregion
    }

    #region Main
    private void MainWeaponActionDown()
    {
        weapons[equippedWeaponIndex].MainActionDown();
    }

    private void MainWeaponActionUp()
    {
        weapons[equippedWeaponIndex].MainActionUp();
    }
    #endregion

    #region Alt
    private void AltWeaponActionDown()
    {
        weapons[equippedWeaponIndex].AltActionDown();
    }

    private void AltWeaponActionUp()
    {
        weapons[equippedWeaponIndex].AltActionUp();
    }
    #endregion
    #endregion

    #region Change Weapon
    private void WeaponChangeInput()
    {
        if (Input.GetKeyDown(equipWeapon1Keycode))
        {
            ChangeEquippedWeapon(0);
        }

        if (Input.GetKeyDown(equipWeapon2Keycode))
        {
            ChangeEquippedWeapon(1);
        }
    }

    private void ChangeEquippedWeapon(int weaponIndex)
    {
        if (weapons.Length <= weaponIndex || weapons[weaponIndex] == null) return;

        equippedWeaponIndex = weaponIndex;
    }
    #endregion
    #endregion

    // TODO
    private void AimWeapon()
    {
        transform.up = aimDir;
        weapons[equippedWeaponIndex].Aim(aimDir);
    }
    #endregion
}
