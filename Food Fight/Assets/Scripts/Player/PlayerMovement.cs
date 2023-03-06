using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Josh Bonovich
//Food Fight
//Player Movement
//This script controls player movement
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10;
    private float horizontalMovement = 0, verticalMovement = 0;
    [SerializeField] string moving;
    [SerializeField] Animator an;
    Rigidbody2D rb;

    [SerializeField] private AudioClip footStepSound;
    [Tooltip("Time between footstep sounds")]
    [SerializeField] float timeBetweenFootstepSounds = 0.25f;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float footstepVolume = 0.6f;

    private float lastStepTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if pushing up
        if (Input.GetKey(KeyCode.W)) verticalMovement = 1;
        //if pushing down
        else if (Input.GetKey(KeyCode.S)) verticalMovement = -1;
        //If both up and down are pressed
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S) || !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))) verticalMovement = 0;

        //if pushing right
        if (Input.GetKey(KeyCode.D)) horizontalMovement = 1;
        //if pushing left
        else if (Input.GetKey(KeyCode.A)) horizontalMovement = -1;
        //If both right and left are pressed
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A) || !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))) horizontalMovement = 0;

        Move();
    }

    private void Move()
    {
        var isMoving = verticalMovement != 0 || horizontalMovement != 0;

        an.SetBool(moving, isMoving);

        if (isMoving && Time.time > timeBetweenFootstepSounds + lastStepTime)
        {
            //print("Step");
            lastStepTime = Time.time;
            SoundManager.PlaySound(footStepSound, footstepVolume, transform.position);
        }

        //transform.Translate((transform.up * verticalMovement + transform.right * horizontalMovement) * speed * Time.deltaTime);
        rb.velocity = (Vector2.up * verticalMovement + Vector2.right * horizontalMovement) * speed;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);
    }

    public void StopPlayer()
    {
        rb.velocity = Vector2.zero;
        this.enabled = false;
    }
}
