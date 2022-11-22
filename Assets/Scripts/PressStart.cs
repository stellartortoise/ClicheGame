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

    [SerializeField]
    private Animator transition;
    [SerializeField]
    private float transitionTime = 0.5f;

    private void Start()
    {
        image = GetComponent<Image>();
        InvokeRepeating("Timer", blinkTime, blinkTime);
        audioSource = GetComponent<AudioSource>();

        //PlayerPrefs.SetInt("Highscore", 0);

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

        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
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
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(StartCrossfade());
    }

    IEnumerator StartCrossfade()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        GoToGame();
    }
}
