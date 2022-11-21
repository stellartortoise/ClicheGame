using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PressStart : MonoBehaviour
{
    private bool opacity = true;
    private Image image;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip clip;
    [SerializeField]
    private float blinkTime = 0.5f, fastBlinkTime = 0.1f;

    private void Start()
    {
        image = GetComponent<Image>();
        InvokeRepeating("Timer", blinkTime, blinkTime);
        audioSource = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        image.enabled = opacity;

        if (Input.GetButtonDown("Submit"))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(clip);
                CancelInvoke("Timer");
                InvokeRepeating("Timer", fastBlinkTime, fastBlinkTime);
                StartCoroutine(GoToNextRoom());
            }
            //image.enabled = false;
            
        }
    }

    void GoToGame()
    {
        SceneManager.LoadScene("Bat Out of Hell");
    }

    void Timer()
    {
        opacity = !opacity;
    }

    IEnumerator GoToNextRoom()
    {
        yield return new WaitForSeconds(0.9f);
        GoToGame();
    }
}
