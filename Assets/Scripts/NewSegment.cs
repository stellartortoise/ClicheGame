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
        //_transform.transform.parent = dolly.transform;

        //_transform.parent = null;

        //Vector3 worldPos = transform.TransformPointUnscaled(_transform.position);

        //GameObject emptyGO = new GameObject(); // Because new Transform can't be made but a transform is a component of a game object
        //Transform newTransform = emptyGO.transform;
        //newTransform.position = worldPos;

        //_transform.position -= dolly.transform.position - new Vector3(0,0,-5);

        if (_transform.GetComponent<UnparentWaypoint>().isStartingPoint == true)
        {
            Vector3 dollyStartPosition = new Vector3(0.23f, 0f, 20.18f); //z  - 5f
            _transform.position -= dollyStartPosition - new Vector3(0, 0, -5f);
        }
        else
        {
            //    //Vector3 dollyStartPosition = new Vector3(0.23f, 0f, 20.18f);// + prevSegment.transform.position; //z  - 5f
            //    //    //_transform.position -= dollyStartPosition - new Vector3(0, 0, -5);
            //    //_transform.position -= dollyStartPosition - new Vector3(0, 0, -5) - prevSegment.transform.position;
            //    //_transform.position += new Vector3(0, 0, prevSegment.transform.position.z);
            //    //_transform.position += new Vector3(0, 0, 28.18f);

            //    //_transform.position = transform.TransformPoint(_transform.position);
            float z_difference = 28.14f;
            Vector3 dollyStartPosition = new Vector3(0.23f, 0f, 20.18f); //z  - 5f
            _transform.position -= dollyStartPosition - new Vector3(0, 0, -5f + (z_difference * iteration));

        }


        gameManager.AddPoint(_transform);
    }
}
