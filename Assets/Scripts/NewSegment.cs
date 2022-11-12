using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSegment : MonoBehaviour
{
    [SerializeField]
    private Transform[] transforms;

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
                _transform.transform.parent = dolly.transform;

                gameManager.AddPoint(_transform);
            }
        }

    }

    IEnumerator GenerateTrackPoints()
    {
        yield return new WaitForSeconds(orderOfCreation);

        foreach (Transform _transform in transforms)
        {
            _transform.transform.parent = dolly.transform;

            gameManager.AddPoint(_transform);
        }
    }
}
