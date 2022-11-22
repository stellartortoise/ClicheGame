using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Some Code Taken from Zigurous's Unity Flappy Bird tutorial (youtube)
    /// </summary>
    public int score, points;
    private int prevScore;

    //public static int staticScore;
    //public static int highScore;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private GameObject gameOver, restartButton, winText, slider, pointMarker, percentComplete, paused, newRecord, howToPlay;
    [SerializeField]
    private BatMovement bat;
    [SerializeField]
    private BatIcon percentage;
    //[SerializeField]
    //private Canvas canvas;
    //[SerializeField]
    //private BatIcon batIcon;
    [SerializeField]
    private AudioManager audioManager;
    [SerializeField]
    private AudioClip clip;

    [SerializeField]
    private Animator animator;

    public Camera _camera;

    [Space]
    [Header("Skybox Stuff")]
    [SerializeField]
    private Material skybox;
    [SerializeField]
    private GameObject directionalLight;
    [SerializeField]
    private CustomImageEffect cameraScript;

    private bool isGameOver = false;
    public bool isPaused = false;
    private bool hasWon = false;
    

    private void Awake()
    {
        //Application.targetFrameRate = 60;

        //Pause();
    }

    private void Start()
    {
        //staticScore = 0;
        //highScore = PlayerPrefs.GetInt("Highscore", highScore);
    }

    public void Restart()
    {
        score = 0;
        scoreText.text = score.ToString();

        restartButton.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 1f; //1
        bat.enabled = true;

        if (hasWon == false)
        {
            //animator.Play("Base Layer.Crossfade_Begin", -1);
            //StartCoroutine(RestartLevel());
            SceneManager.LoadScene("Bat Out of Hell");
        }
        else
        {
            //animator.Play("Base Layer.Crossfade_Begin", -1);
            //StartCoroutine(GoToMenu());
            SceneManager.LoadScene("Menu");
        }
        


    }

    public void Pause()
    {
        Time.timeScale = 0;
        bat.enabled = false;
    }

    private void Paused()
    {
        Time.timeScale = 0;
        paused.SetActive(true);
    }

    public void GameOver()
    {
        isGameOver = true;

        //RenderSettings.skybox = skybox;
        cameraScript.ScanDistance = -10f;
        directionalLight.SetActive(true);

        gameOver.SetActive(true);
        restartButton.SetActive(true);
        slider.SetActive(false);
        howToPlay.SetActive(false);

        
        percentComplete.SetActive(true);

        Pause();

        //if (staticScore > highScore)
        //{
        //    highScore = staticScore;

        //    PlayerPrefs.SetInt("Highscore", highScore);

        //    newRecord.SetActive(true);
        //}
    }

    public void IncreaseScore()
    {
        //prevScore = score;
        score += points;
        //int finalScore = (int)Mathf.Lerp(prevScore, score, 0.1f);
        scoreText.text = score.ToString();
    }

    public void Win()
    {
        slider.SetActive(false);
        pointMarker.SetActive(false);
        winText.SetActive(true);

        gameOver.SetActive(true);
        restartButton.SetActive(true);
        percentComplete.SetActive(true);

        isGameOver = true;
        hasWon = true;

        _camera.transform.parent = null;
    }

    private void Update()
    {
        if (isGameOver)
        {
            if (Input.GetButtonDown("Submit")) //(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit"))
            {
                //if (!audioManager.audioSource.isPlaying)
                //{
                //    audioManager.audioSource.PlayOneShot(clip);
                //}
                Restart();
            }
        }
        
        if (isGameOver == false && Input.GetButtonDown("Submit"))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Paused();
            }
            else
            {
                Time.timeScale = 1;
                paused.SetActive(false);
            }

            if (!audioManager.audioSource.isPlaying)
            {
                audioManager.audioSource.PlayOneShot(clip);
            }
        }

        //staticScore = Mathf.RoundToInt(percentage.percentage);

    }

    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Menu");
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Bat Out of Hell");
    }
}
