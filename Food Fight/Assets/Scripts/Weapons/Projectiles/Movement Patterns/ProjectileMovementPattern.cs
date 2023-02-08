/*
 * (Jacob Welch)
 * (ProjectileMovementPattern)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovementPattern : MonoBehaviour
{
    #region Fields
    private Vector2 startingPosition;

    private Rigidbody2D rigidbody2d;
    #endregion

    #region Functions
    /// <summary>
    /// Handles initilization of components and other fields before anything else.
    /// </summary>
    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    public virtual void MovementTick(float speed, float range)
    {
        rigidbody2d.MovePosition(rigidbody2d.position + (Vector2)(transform.right * Time.fixedDeltaTime * speed));

        if (Vector2.Distance(startingPosition, transform.position) > range)
        {
            GetComponent<Projectile>().MaxRangeReached();
        }
    }
    #endregion
}
