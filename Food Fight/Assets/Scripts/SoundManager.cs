/*
 * (Jacob Welch)
 * (SoundManager)
 * (Food Fight)
 * (Description: Handles sound being played an allows for scaling of volume over distance.)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Fields
    // Components
    private AudioSource audioSource;
    private static SoundManager instance;

    /// <summary>
    /// The main camera's transform.
    /// </summary>
    private Transform cameraTransform;
    
    [Range(0.0f, 200.0f)]
    [Tooltip("The minimum distance for sound scaling")]
    [SerializeField] private float minDist = 1;

    [Range(0.0f, 600.0f)]
    [Tooltip("The maximum distance for sound scaling")]
    [SerializeField] private float maxDist = 400;
    #endregion

    #region Functions
    /// <summary>
    /// Handles initilization of components and other fields before anything else.
    /// </summary>
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        cameraTransform = Camera.main.transform;
        instance = this;
    }

    /// <summary>
    /// Plays a sound and scales it's volume based on the distance to the camera.
    /// </summary>
    /// <param name="soundClip">The sound clip to be played.</param>
    /// <param name="volume">The volume of the sound clip to be played.</param>
    /// <param name="pos">The position to "play" the sound clip at.</param>
    public static void PlaySound(AudioClip soundClip, float volume, Vector2 pos)
    {
        if (instance == null || instance.audioSource == null || soundClip == null || instance.cameraTransform == null) return;

        float dist = Vector2.Distance(pos, instance.cameraTransform.position);

        #region Scaling volume over distance
        if (dist > instance.maxDist)
        {
            volume = 0;
        }
        else
        {
            volume *= 1 - ((dist - instance.minDist) / (instance.maxDist - instance.minDist));
        }
        #endregion

        instance.audioSource.PlayOneShot(soundClip, volume);
    }
    #endregion
}
