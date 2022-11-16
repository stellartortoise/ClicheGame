using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    /// <summary>
    /// Some of this code taken from youtuber Mix and Jam
    /// </summary>
    public float xySpeed = 18f;
    public Transform aimTarget;
    public float lookSpeed;

    private float grav = 0.01f;
    private float yy;

    private Rigidbody rb;

    private Transform playerModel;

    private float mZCoord;
    private float startz;

    [Space]
    [Header("Sound Effects")]

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioSource audioSourceNoEcho;

    [SerializeField]
    private AudioClip[] clips;

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
    //private int segmentLength;
    //private int wallLength;

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

        //ResetSpawnPoints();

    }

    //private void ResetSpawnPoints()
    //{
    //    //Get rid of spawn position object for spawning in new segments
    //    if (spawnPosition != null)
    //    {
    //        Destroy(spawnPosition);
    //        spawnPosition = null;
    //    }

    //    if (wallSpawnPosition != null)
    //    {
    //        Destroy(wallSpawnPosition);
    //        wallSpawnPosition = null;
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");// Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Vertical");// Input.GetAxis("Mouse Y");

        LocalMove(h, v, xySpeed);
        //RotationLook(h, v, lookSpeed);
        HorizontalLean(playerModel, h, 45, 0.1f);

        //var tempGrav = grav;
        //if (Input.GetKeyDown(KeyCode.Space))
        //{

        //    grav = 0;
        //    //yy += 2f;
        //    transform.localPosition += new Vector3(0, 2f, 0);

        //}
        //grav = tempGrav;
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            //rb.AddForce(new Vector3(0, 5f, 0), ForceMode.Force);
            rb.velocity += Vector3.up * 15f;

            if (audioSourceNoEcho.isPlaying)
            {
                audioSourceNoEcho.Stop();
            }

            int getClipNumber = UnityEngine.Random.Range(0, clips.Length);
            audioSourceNoEcho.PlayOneShot(clips[getClipNumber]);
        }
        //Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        //if (transform.localPosition.y > pos.y)
        //{
        //    yy -= grav;
        //}

    }

    //private void LateUpdate()
    //{
    //    Vector3 viewPos = transform.position;

    //    if (transform.localPosition.x < (screenBounds.x * -1) + 1)
    //    {
    //        rb.velocity = new Vector3(0, rb.velocity.y, 0);
    //    }
    //}

    private void LocalMove(float x, float y, float speed)
    {
        //transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;

        rb.AddForce(Vector3.right * x * speed);
        //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 10);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 5f); //transform.localPosition.y
                                                                                 //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, transform.parent.position.z);


        //float xx = -(transform.localPosition.x - Input.mousePosition.x) * 0.1f;
        //float xx = Mathf.Lerp(transform.localPosition.x, Input.mousePosition.x, 0.0001f); //Mouse Position TAKES INTO ACCOUNT BOTH MONITORS
        //float yy = rb.velocity.y;

        //transform.localPosition = Vector3.Lerp(new Vector3(transform.localPosition.x, transform.localPosition.y, 4f), GetMouseWorldPosTarget(), 0.01f);
        //transform.localPosition = new Vector3(xx, 0, 0);
        
        
        //ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
        
    }

    void RotationLook(float h, float v, float speed)
    {
        ////aimTarget.parent.position = Vector3.zero;
        //aimTarget.localPosition = new Vector3(h, v, 5f);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(GetMouseWorldPosTarget()), Mathf.Deg2Rad * speed);

        //aimTarget.parent.position = Vector3.zero;
        //aimTarget.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(h, v, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speed * Time.deltaTime);
    }

    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngles = target.eulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y, 0);// Mathf.LerpAngle(targetEulerAngles.z, -axis * leanLimit, lerpTime));
        target.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(targetEulerAngles.z, -axis * leanLimit, lerpTime));// Mathf.LerpAngle(targetEulerAngles.z, -axis * leanLimit, lerpTime));
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(aimTarget.position, 0.5f);
    //    Gizmos.DrawSphere(aimTarget.position, 0.15f);
    //}

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
        //Debug.Log("Works");
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
        gameManager.GameOver();
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Score")
    //    {
    //        iteration++;

    //        int si = UnityEngine.Random.Range(0, segments.Count);
    //        int wi = UnityEngine.Random.Range(0, walls.Count);
    //        //isInScoreZone = true;
    //        //gameManager.points = 15;
    //        //gameManager.IncreaseScore();
    //        spawnPosition = GameObject.FindGameObjectWithTag("Spawner");
    //        wallSpawnPosition = GameObject.FindGameObjectWithTag("WallSpawner");

    //        GameObject go = Instantiate(segments[si]);
    //        go.transform.position = new Vector3(spawnPosition.transform.position.x, 2.21f, spawnPosition.transform.position.z); //31.46f
    //        go.GetComponent<NewSegment>().prevSegment = prevSegment;
    //        go.GetComponent<NewSegment>().iteration = iteration;

    //        GameObject go2 = Instantiate(walls[wi]);
    //        go2.transform.position = new Vector3(wallSpawnPosition.transform.position.x, 0.63f, wallSpawnPosition.transform.position.z);
    //        go2.GetComponent<NewSegment>().prevSegment = prevSegment;
    //        go2.GetComponent<NewSegment>().iteration = iteration;
    //        //go.dolly = dolly;

    //        prevSegment = go;

    //        ResetSpawnPoints();
    //        Destroy(other);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Score")
    //    {
    //        isInScoreZone = false;
    //    }
    //}
}
