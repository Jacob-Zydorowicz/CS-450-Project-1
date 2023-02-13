/*
 * Jacob Zydorowicz
 * DestructableTiles.cs
 * FoodFight
 * Code used from youtube tutorial at https://www.youtube.com/watch?v=94KWSZBSxIA in order to make destructable tiles in map.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructableTiles : MonoBehaviour
{
    public Tilemap destructableTiles;

    private void Start()
    {
        destructableTiles = GetComponent<Tilemap>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Projectiles"))
        {
            Debug.Log("hit");
            Vector3 hitPos = Vector3.zero;

            foreach(ContactPoint2D hit in collision.contacts)
            {
                hitPos.x = hit.point.x - 0.01f * hit.normal.x;
                hitPos.y= hit.point.y - 0.01f * hit.normal.y;

                destructableTiles.SetTile(destructableTiles.WorldToCell(hitPos), null);
            }
        }
    }
}
