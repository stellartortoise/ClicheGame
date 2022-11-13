using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnparentWaypoint : MonoBehaviour
{

    void Awake()
    {
        this.transform.parent = null;
    }


}
