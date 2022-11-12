using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CavernCreation : MonoBehaviour
{

    public CinemachineSmoothPath smoothPath;
    //public Transform[] transformWaypoints;
    public List<Transform> transformWaypoints = new List<Transform>();


    void Start()
    {
        CreateWaypoints();
    }

    public void AddPoint(Transform newPoint)
    {
        transformWaypoints.Add(newPoint);

        CreateWaypoints();
        
    }

    void CreateWaypoints()
    {
        smoothPath.m_Waypoints = new CinemachineSmoothPath.Waypoint[transformWaypoints.Count];

        for (int i = 0; i < transformWaypoints.Count; i++)
        {
            smoothPath.m_Waypoints[i] = new CinemachineSmoothPath.Waypoint();
            smoothPath.m_Waypoints[i].position = transformWaypoints[i].localPosition;
        }

        smoothPath.InvalidateDistanceCache();

        //Debug.Log(smoothPath.);
    }


}
