using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    /// <summary>
    /// Some of this code taken from youtuber Mix and Jam
    /// </summary>
    public float xySpeed = 18f;// 18f;
    public Transform aimTarget;
    public float lookSpeed;

    private bool won = false;

    private float startx, prevx;
    private Vector3 leftPos, rightPos, centrePos;

    private float grav = 0.01f;
    private float yy;

    private Rigidbody rb;

    private Transform playerModel;

    private float mZCoord;
    private float startz;
    [Space]
    [Header("Sound Effects")]

    [SerializeField]
    private Animator animator;

    [Space]
    [Header("Sound Effects")]

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioSource audioSourceNoEcho;

    [SerializeField]
    private AudioClip[] clips;

    [SerializeField]
    private AudioClip[] crashClips;

    [Space]

    [SerializeField]
    private GameManager gameManager;

    private Vector3 screenBounds;

    private bool isInScoreZone = false;

    [Space]
    [Header("List of Prefabs to Spawn")]
    [SerializeField]
    private List<GameObject> segments = new List<GameObject>();
    [SerializeField]
    private List<GameObject> walls = new List<GameObject>();
    [SerializeField]
    private GameObject dolly;

    private GameObject spawnPosition;
    private GameObject wallSpawnPosition;

    [SerializeField]
    private GameObject prevSegment;

    private int iteration = 0;


    // Start is called before the first frame update
    void Start()
    {
        playerModel = transform.GetChild(0);
        startz = Camera.main.transform.position.z - 7f;

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        StartCoroutine(startTime());

        yy = transform.localPosition.y;

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        InvokeRepeating("Score", 4, 4);

        startx = transform.localPosition.x;

        leftPos = new Vector3(startx - 0.5f, transform.position.y, 5f);
        rightPos = new Vector3(startx + 0.5f, transform.position.y, 5f);
        centrePos = new Vector3(startx, transform.position.y, 5f);


    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");// Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Vertical");// Input.GetAxis("Mouse Y");

        if (won == false && gameManager.isPaused == false)
        {
            LocalMove(h, v, xySpeed);
            HorizontalLean(playerModel, h, 45, 0.1f);

            if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
            {
                //rb.AddForce(new Vector3(0, 5f, 0), ForceMode.Force);
                rb.velocity += Vector3.up * 7f; //15
                

                //if (audioSourceNoEcho.isPlaying)
                //{
                //    audioSourceNoEcho.Stop();
                //}

                animator.Play("Flying", -1, 0f);
                int getClipNumber = UnityEngine.Random.Range(0, clips.Length);
                audioSourceNoEcho.PlayOneShot(clips[getClipNumber]);
            }

        }

        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }

    private void LocalMove(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, 0, 0) * speed * Time.deltaTime;
        x = Mathf.Clamp(x, -0.5f, 0.5f);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 5f); //Original Movement Line
    }


    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngles = target.eulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y, 0);// Mathf.LerpAngle(targetEulerAngles.z, -axis * leanLimit, lerpTime));
        target.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(targetEulerAngles.z, -axis * leanLimit, lerpTime));// Mathf.LerpAngle(targetEulerAngles.z, -axis * leanLimit, lerpTime));
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = 4f; // Camera.main.transform.position.z - 7f; //startz;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private Vector3 GetMouseWorldPosTarget()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = Camera.main.transform.position.z + 4f; //7

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    IEnumerator startTime()
    {
        yield return new WaitForSeconds(0.1f); //0.5f
        rb.useGravity = true;
    }

    private void Score()
    {
        if (isInScoreZone == false)
        {
            gameManager.points = 1;
            gameManager.IncreaseScore();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!won)
        {
            int i = UnityEngine.Random.Range(0, crashClips.Length);

            audioSourceNoEcho.PlayOneShot(crashClips[i]);
            audioSource.Stop();
            gameManager.GameOver();
        }

    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "LevelEnd")
        {
            gameManager.Win();
            won = true;
            rb.useGravity = false;
            animator.SetBool("GameEnd", true);
        }

    }

}
