using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health;
    public float damage;
    public float speed;

    public GameObject player;

    private float distance;

    //public GameObject projectile;
    //public Transform projectilePos;

    //private float timer;

    // Start is called before the first frame update
    void Start()
    {
        health = 10;
        damage = 10;
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        //timer += Time.deltaTime;

        //float dist = Vector2.Distance(transform.position, player.transform.position);

        //if (dist < 10)
        //{
        //    timer += Time.deltaTime;

        //    if (timer > 2)
        //    {
        //        timer = 0;
        //        Shoot();
        //    }
        //}
    }

    public void Movement()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < 4)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }

    //public void Shoot()
    //{
    //    Instantiate(projectile, projectilePos.position, Quaternion.identity);
    //}
}
