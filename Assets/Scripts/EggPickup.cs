using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggPickup : MonoBehaviour
{

    private Vector3 mOffset;
    private Vector3 startingPos;
    private Vector3 endPos;

    private Rigidbody rb;
    private float yy = 1f;

    [SerializeField]
    private EggMaster eggMaster;

    public bool replacementEgg;

    private float mZCoord;
    private bool dragging = false;
    private bool lerpToSpot = false;
    private Vector3 spot;
    [SerializeField] private GameObject myStartingZone;
    [SerializeField] private GameObject endZone;

    private void Awake()
    {
        startingPos = transform.position;
        rb = GetComponent<Rigidbody>();

        spot = startingPos;
        endPos = endZone.transform.position;
    }

    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        mOffset = gameObject.transform.position - GetMouseWorldPos();

        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = Vector3.Lerp(transform.position, GetMouseWorldPos() + mOffset + new Vector3(0, yy, 0), 0.3f);
    }

    private void Update()
    {
        if (!replacementEgg)
        {
            if (Vector3.Distance(transform.position, startingPos) < 2f)
            {
                lerpToSpot = true;
                spot = startingPos;
            }
            else if (Vector3.Distance(transform.position, endPos) < 2f)
            {
                lerpToSpot = true;
                spot = endPos;
            }
            else
            {
                lerpToSpot = false;
            }
        }
        else
        {
            //if (eggMaster.eggs.Count > -1)
            //{
            //    foreach (var egg in eggMaster.eggs)
            //    {
            //        if (Vector3.Distance(transform.position, egg) < 2f)
            //        {
            //            lerpToSpot = true;
            //            spot = egg;
            //        }
            //        else
            //        {
            //            lerpToSpot = false;
            //        }
            //    }
            //}

            if (eggMaster.eggs.Count == 1)
            {
                if (Vector3.Distance(transform.position, eggMaster.eggs[0]) < 2f)
                {
                    lerpToSpot = true;
                    spot = eggMaster.eggs[0];
                }
                else
                {
                    lerpToSpot = false;
                }
            }

            if (eggMaster.eggs.Count == 2)
            {
                if (Vector3.Distance(transform.position, eggMaster.eggs[0]) < 2f)
                {
                    lerpToSpot = true;
                    spot = eggMaster.eggs[0];
                }
                else if (Vector3.Distance(transform.position, eggMaster.eggs[1]) < 2f)
                {
                    lerpToSpot = true;
                    spot = eggMaster.eggs[1];
                }
                else
                {
                    lerpToSpot = false;
                }
            }

            if (eggMaster.eggs.Count == 3)
            {
                if (Vector3.Distance(transform.position, eggMaster.eggs[0]) < 2f)
                {
                    lerpToSpot = true;
                    spot = eggMaster.eggs[0];
                }
                else if (Vector3.Distance(transform.position, eggMaster.eggs[1]) < 2f)
                {
                    lerpToSpot = true;
                    spot = eggMaster.eggs[1];
                }
                else if (Vector3.Distance(transform.position, eggMaster.eggs[2]) < 2f)
                {
                    lerpToSpot = true;
                    spot = eggMaster.eggs[2];
                }
                else
                {
                    lerpToSpot = false;
                }
            }

            if (eggMaster.eggs.Count == 4)
            {
                if (Vector3.Distance(transform.position, eggMaster.eggs[0]) < 2f)
                {
                    lerpToSpot = true;
                    spot = eggMaster.eggs[0];
                }
                else if (Vector3.Distance(transform.position, eggMaster.eggs[1]) < 2f)
                {
                    lerpToSpot = true;
                    spot = eggMaster.eggs[1];
                }
                else if (Vector3.Distance(transform.position, eggMaster.eggs[2]) < 2f)
                {
                    lerpToSpot = true;
                    spot = eggMaster.eggs[2];
                }
                else if (Vector3.Distance(transform.position, eggMaster.eggs[3]) < 2f)
                {
                    lerpToSpot = true;
                    spot = eggMaster.eggs[3];
                }
                else
                {
                    lerpToSpot = false;
                }
            }
        }

        if (dragging == false)
        {
            //transform.Translate(startingPos);
            if (lerpToSpot == true)
            {
                rb.useGravity = false;
                transform.position = Vector3.Lerp(transform.position, spot, 0.3f);
            }
            else
            {
                if (transform.position != spot)
                {
                    rb.useGravity = true;
                }
            }
            
        }

        //Debug.Log(dragging);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "StartingZone")
        //{
        //    if (other.gameObject == myStartingZone)
        //    {
        //        lerpToSpot = true;

        //        spot = startingPos;
        //    }
        //}

        if (other.tag == "EndZone")
        {
            //if (other.gameObject == endZone)
            //{
            //    //lerpToSpot = true;

            //    //spot = endPos;


            //}
            eggMaster.eggs.Add(startingPos);
            Debug.Log(startingPos);
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    lerpToSpot = false;
    //}
}
