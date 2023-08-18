using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S3CommonFunctions : S3Adder
{
    // returns spherical distance
    public float SphDistance(Vector4 a, Vector4 b) {
        return Mathf.Acos(Vector3.Dot(a, b));
    }
    
    // lerps between a b in spherical space
    public Vector4 SphLerp(Vector4 a, Vector4 b, float t) {
        float d = Mathf.Acos(Vector3.Dot(a, b)) * t;
        E4Rotation q = E4Rotation.AnglePlane(d, a, b);
        return q.RotateVector(a);
    }

    // returns quaternion that does lerping
    public E4Rotation LerpQuaternion(Vector4 a, Vector4 b, float t) {
        float d = Mathf.Acos(Vector3.Dot(a, b)) * t;
        E4Rotation q = E4Rotation.AnglePlane(d, a, b);
        return q;
    }

    // calculates position where ray is after distance t
    public Vector4 RayTravel(Vector4 o, Vector4 d, float t) {
        return E4Rotation.AnglePlane(t, o, d).RotateVector(o);
    }

    // returns points on line on sphere
    public Vector4[] GetLinePoints(Vector4 v0, Vector4 v1, int points_n=20) {
        float length = Mathf.Acos(Vector3.Dot(v0, v1));
        
        E4Rotation q = E4Rotation.AnglePlane(length / (float)points_n - 1, v0, v1);
        E4Rotation newQ = q.Copy();
        Vector4[] points = new Vector4[points_n];

        points[0] = v0;
        for (int n = 0; n < points_n-1; n++) {
            points[n+1] = newQ.RotateVector(v0);
            newQ.ApplyRotationOnSelf(q);
        }

        return points;
    }
}
