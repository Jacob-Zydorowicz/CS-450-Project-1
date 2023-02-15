/*
 * (Jacob Welch)
 * (BeamWeaponAction)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamWeaponAction : WeaponAction
{
    #region Fields
    private LayerMask enemyMask;
    #endregion

    #region Functions
    protected override void Awake()
    {
        base.Awake();
        enemyMask = LayerMask.NameToLayer("Enemy");
    }

    public override void PerformAction()
    {
        base.PerformAction();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, Range, enemyMask);

        Debug.DrawRay(transform.position, transform.right, Color.green);

        if(hit.transform != null)
        {
            var damageable = hit.transform.gameObject.GetComponent<Damageable>();

            if(damageable != null)
            damageable.UpdateHealth(Damage);
        }
    }
    #endregion
}
