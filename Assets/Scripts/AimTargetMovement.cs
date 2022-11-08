using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTargetMovement : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        mOffset = transform.position - GetMouseWorldPos();

        transform.position = mOffset; //Vector3.Lerp(transform.position, GetMouseWorldPos() + mOffset, 0.3f);// + new Vector3(0, 1f, 0), 0.3f);
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
