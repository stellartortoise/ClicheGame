using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnparentWaypoint : MonoBehaviour
{
    public bool isStartingPoint = false;
    void Awake()
    {
        this.transform.parent = null;
    }


}
