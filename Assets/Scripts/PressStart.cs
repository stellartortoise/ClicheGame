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

    private void Start()
    {
        image = GetComponent<Image>();
        InvokeRepeating("Timer", 0.5f, 0.5f);
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
                StartCoroutine(GoToNextRoom());
            }
            image.enabled = false;
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
        yield return new WaitForSeconds(0.5f);
        GoToGame();
    }
}
