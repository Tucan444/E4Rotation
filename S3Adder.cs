using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S3Adder : S3Converter
{

    // add converts second vector to spherical point and adds it as 3 rotations
    public Vector4 AddCartCart(Vector4 c, Vector4 s) {
        E4Rotation r = E4Rotation.Point(s);
        return r.RotateVector(c);
    }

    /* // add spherical point as 2 rotations
    public Vector2 AddSpherSpher(Vector2 a, Vector2 b) {
        return Cartesian2Spherical(AddCartSpher(Spherical2Cartesian(a), b));
    }

    // substract spherical point as 2 rotations
    public Vector3 SubstractCartSpher(Vector3 c, Vector2 s){
        s = CrampSpherical(s);
        float rAngle = Mathf.Abs(s[1]) * (180 / Mathf.PI);
        Quaternion rot = Quaternion.AngleAxis(-rAngle, Vector3.Cross(Spherical2Cartesian(new Vector2(s[0], 0)), Spherical2Cartesian(s)));

        Vector2 substracted = Cartesian2Spherical(rot * c);
        substracted[0] -= s[0];

        return Spherical2Cartesian(substracted);
    }
    
    // add spherical point as 2 rotations
    public Vector2 AddPolarSpher(Vector2 p, Vector2 s) {
        return Cartesian2Polar(AddCartSpher(Polar2Cartesian(p), s));
    }

    public Vector2 SubstractPolarSpher(Vector2 p, Vector2 s) {
        return Cartesian2Polar(SubstractCartSpher(Polar2Cartesian(p), s));
    }

    // puts angles in between -pi and pi
    public Vector2 CrampSpherical(Vector2 s) {
        for (int i = 0; i < 2; i++) {
            if (s[i] > Mathf.PI) {
                while (s[i] > Mathf.PI) {
                    s[i] -= TAU;
                }
            } else if (s[i] < -Mathf.PI) {
                while (s[i] < -Mathf.PI) {
                    s[i] += TAU;
                }
            }
        }
        return s;
    } */
}
