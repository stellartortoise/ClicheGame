using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    // THIS CODE IS THANKS TO "villevli" ON ANSWERS.UNITY

    public static Vector3 TransformPointUnscaled(this Transform transform, Vector3 position)
    {
        var localToWorldMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        return localToWorldMatrix.MultiplyPoint3x4(position);
    }

    public static Vector3 InverseTransformPointUnscaled(this Transform transform, Vector3 position)
    {
        var localToWorldMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one).inverse;
        return localToWorldMatrix.MultiplyPoint3x4(position);
    }
}
