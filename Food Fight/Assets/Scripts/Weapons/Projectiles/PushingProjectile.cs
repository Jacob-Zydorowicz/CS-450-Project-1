/*
 * (Jacob Welch)
 * (PushingProjectile)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingProjectile : Projectile
{
    #region Fields
    private float knockbackForce = 15.0f;
    #endregion

    #region Functions
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(!ShouldIgnore(collision.gameObject) && collision.gameObject.TryGetComponent(out Rigidbody2D rb2d))
        {
            var dir = collision.transform.position - transform.position;
            rb2d.AddForce(knockbackForce*dir.normalized);
        }

        base.OnTriggerEnter2D(collision);
    }
    #endregion
}
