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

    private Transform playerModel;

    private float mZCoord;
    private float startz;

    // Start is called before the first frame update
    void Start()
    {
        playerModel = transform.GetChild(0);
        startz = Camera.main.transform.position.z - 7f;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        LocalMove(h, v, xySpeed);
        RotationLook(h, v, lookSpeed);
        HorizontalLean(playerModel, h, 80, 0.1f);
        
    }

    private void LocalMove(float x, float y, float speed)
    {
        //transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
        transform.localPosition = Vector3.Lerp(transform.localPosition, GetMouseWorldPos(), 0.01f);
        ClampPosition();
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
        //aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(h, v, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(GetMouseWorldPosTarget()), Mathf.Deg2Rad * speed);
    }

    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngles = target.eulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y, Mathf.LerpAngle(targetEulerAngles.z, -axis * leanLimit, lerpTime));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, 0.5f);
        Gizmos.DrawSphere(aimTarget.position, 0.15f);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = Camera.main.transform.position.z - 7f; //startz;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private Vector3 GetMouseWorldPosTarget()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = Camera.main.transform.position.z + 7f;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

}
