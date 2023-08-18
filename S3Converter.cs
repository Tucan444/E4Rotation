using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S3Converter
{
    // polar scheme xzzyzw

    public float Rad2Deg = 180 / Mathf.PI;
    public float Deg2Rad = Mathf.PI / 180;
    public float TAU = 2 * Mathf.PI;
    public float HalfPI = Mathf.PI * 0.5f;

    // converts spherical position to cartesian
    public Vector4 Spherical2Cartesian(Vector3 v){ // working
        Vector4 position = new Vector4();
        
        position[0] = (float)(Math.Cos(v[0]) * Math.Cos(v[1])) * Mathf.Cos(v[2]);
        position[2] = (float)(Math.Sin(v[0]) * Math.Cos(v[1])) * Mathf.Cos(v[2]);
        position[1] = (float)(Math.Sin(v[1])) * Mathf.Cos(v[2]);
        position[3] = Mathf.Sin(v[2]);

        return position;
    }

    // converts spherical to polar
    public Vector3 Spherical2Polar(Vector3 v) {
        return Cartesian2Polar(Spherical2Cartesian(v));
    }

    // converts cartesian to spherical
    public Vector3 Cartesian2Spherical(Vector4 v) {
        Vector3 position = new Vector3();

        position[2] = Mathf.Asin(v[3]);
        float cosOfW = Mathf.Cos(position[2]);

        if (Mathf.Abs(cosOfW) < 0.00001f) {return position;}
        v /= cosOfW;

        position[1] = Mathf.Asin(v[1]);
        float cosOfz = Mathf.Cos(position[1]);
        if (Mathf.Abs(cosOfz) < 0.00001f) {return position;}
        position[0] = Mathf.Atan2(v[2] / cosOfz, v[0] / cosOfz);

        return position;
    }

    // converts catresion to polarSpherical
    public Vector3 Cartesian2Polar(Vector4 v) {
        float alpha = Mathf.Acos(Vector4.Dot(v, new Vector4(1, 0, 0, 0)));
        Vector4 back = E4Rotation.AnglePlane(Mathf.PI*0.5f - alpha, new Vector4(1, 0, 0, 0), v).RotateVector(v);

        float beta = Mathf.Asin(back.y);
        float gamma = 0;

        if (back.y != 1) {
            Vector2 planar = new Vector2(back.z, back.w).normalized;
            gamma = Mathf.Atan2(planar.y, planar.x);
        }

        return new Vector3(alpha, beta, gamma);
    }

    // converts polarSpherical to cartesian
    public Vector4 Polar2Cartesian(Vector3 v) {
        E4Rotation a = E4Rotation.Plane(v.x, "xz");
        E4Rotation b = E4Rotation.Plane(v.y, "zy");
        E4Rotation c = E4Rotation.Plane(v.z, "zw");

        a.ApplyRotationOnSelf(b);
        a.ApplyRotationOnSelf(c);

        return a.RotateVector(new Vector4(1, 0, 0, 0));
    }

    // converts polarSpherical to spherical
    public Vector3 Polar2Spherical(Vector3 v) {
        return Cartesian2Spherical(Polar2Cartesian(v));
    }





    // Used for R3 not S2
    public Vector3 SphericalToCartesian(Vector3 v){ // working
        Vector3 position = new Vector3();
        
        position[0] = (float)(Math.Cos(v[1]) * Math.Cos(v[2]));
        position[2] = (float)(Math.Sin(v[1]) * Math.Cos(v[2]));
        position[1] = (float)(Math.Sin(v[2]));

        position *= v[0];

        return position;
    }

    // Used for R3 not S2
    public Vector3 CartesianToSpherical(Vector3 v) {
        Vector3 position = new Vector3(0, 0, 0);
        Vector3 v_ = new Vector3(v[0], v[1], v[2]);

        position[0] = v.magnitude;
        v_ /= position[0];
        position[2] = Mathf.Asin(v_[1]);
        float cosOfz = Mathf.Cos(v_[1]);
        position[1] = Mathf.Atan2(v_[2] / cosOfz, v_[0] / cosOfz);

        return position;
    }
}
