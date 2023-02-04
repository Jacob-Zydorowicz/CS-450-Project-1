/*
 * (Jacob Welch)
 * (Projectile)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    #region Fields
    [Range(0.0f, 50.0f)]
    [Tooltip("The speed of the projectile")]
    [SerializeField] private float speed = 5;

    private float range = 0;

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

    public void Initialize(float range)
    {
        this.range = range;
    }

    private void FixedUpdate()
    {
        MovementPattern();
    }

    protected virtual void MovementPattern()
    {
        rigidbody2d.MovePosition(rigidbody2d.position + (Vector2)(transform.right * Time.fixedDeltaTime * speed));

        if(Vector2.Distance(startingPosition, transform.position) > range)
        {
            MaxRangeReached();
        }
    }

    protected virtual void MaxRangeReached()
    {
        Destroy(gameObject);
    }
    #endregion
}
