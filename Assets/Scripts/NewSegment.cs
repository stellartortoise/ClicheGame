using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSegment : MonoBehaviour
{
    [SerializeField]
    private Transform[] transforms;

    //public Vector3[] points;
    public GameObject prevSegment;

    public GameObject dolly;

    [SerializeField]
    private CavernCreation gameManager;

    [SerializeField]
    private float orderOfCreation;

    public int iteration;

    private void Awake()
    {
        dolly = GameObject.FindGameObjectWithTag("Dolly");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CavernCreation>();
    }

    private void Start()
    {
        

        if (orderOfCreation > 0)
        {
            StartCoroutine(GenerateTrackPoints());
        }
        else
        {
            foreach (Transform _transform in transforms)
            {
                AddPoint(_transform);
            }
        }

    }

    IEnumerator GenerateTrackPoints()
    {
        yield return new WaitForSeconds(orderOfCreation);

        foreach (Transform _transform in transforms)
        {
            AddPoint(_transform);
        }
    }

    void AddPoint(Transform _transform)
    {

        if (_transform.GetComponent<UnparentWaypoint>().isStartingPoint == true)
        {
            Vector3 dollyStartPosition = new Vector3(0.23f, 0f, 20.18f); //z  - 5f
            _transform.position -= dollyStartPosition - new Vector3(0, 0, -5f);
        }
        else
        {
            float z_difference = 26f; // 33, 28.14f;
            Vector3 dollyStartPosition = new Vector3(0.23f, 0f, 20.18f); //z  - 5f
            _transform.position -= dollyStartPosition - new Vector3(0, 0, -5f + (z_difference * iteration));

        }


        gameManager.AddPoint(_transform);
    }
}
