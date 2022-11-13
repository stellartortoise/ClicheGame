using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSegment : MonoBehaviour
{
    [SerializeField]
    private Transform[] transforms;

    //public Vector3[] points;

    public GameObject dolly;

    [SerializeField]
    private CavernCreation gameManager;

    [SerializeField]
    private float orderOfCreation;

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

        gameManager.AddPoint(_transform);
    }
}
