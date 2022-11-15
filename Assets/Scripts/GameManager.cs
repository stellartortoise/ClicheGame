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

    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private GameObject gameOver, restartButton;
    [SerializeField]
    private BatMovement bat;

    [Space]
    [Header("Skybox Stuff")]
    [SerializeField]
    private Material skybox;
    [SerializeField]
    private GameObject directionalLight;
    [SerializeField]
    private CustomImageEffect cameraScript;

    private bool isGameOver = false;
    

    private void Awake()
    {
        //Application.targetFrameRate = 60;

        //Pause();
    }

    public void Restart()
    {
        score = 0;
        scoreText.text = score.ToString();

        restartButton.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 1f;
        bat.enabled = true;

        SceneManager.LoadScene("Bat Out of Hell");


    }

    public void Pause()
    {
        Time.timeScale = 0;
        bat.enabled = false;
    }

    public void GameOver()
    {
        isGameOver = true;

        //RenderSettings.skybox = skybox;
        cameraScript.ScanDistance = -10f;
        directionalLight.SetActive(true);

        gameOver.SetActive(true);
        restartButton.SetActive(true);

        Pause();
    }

    public void IncreaseScore()
    {
        //prevScore = score;
        score += points;
        //int finalScore = (int)Mathf.Lerp(prevScore, score, 0.1f);
        scoreText.text = score.ToString();
    }

    private void Update()
    {
        if (isGameOver)
        {
            if (Input.GetButtonDown("Submit")) //(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit"))
            {
                Restart();
            }
        }
    }
}
