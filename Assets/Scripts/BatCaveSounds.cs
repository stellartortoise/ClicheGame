using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCaveSounds : MonoBehaviour
{
    public AudioClip[] batSounds;
    [SerializeField]
    private Transform bat;
    [SerializeField]
    private AudioSource audioSource;

    private float distance;

    private void Start()
    {
        //audioSource
    }

    private void Update()
    {

        distance = Vector3.Distance(bat.position, transform.position);

        if (distance < 2f)
        {
            if (!audioSource.isPlaying)
            {
                int random = Random.Range(0, batSounds.Length);

                audioSource.PlayOneShot(batSounds[random]);
            }
        }
    }
}
