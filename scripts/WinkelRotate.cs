using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinkelRotate : MonoBehaviour
{
    void Update()
    {
        double myFaceAngle = DMT.StaticStore.FaceAngle;
        this.transform.rotation = Quaternion.Euler(0, 0, -1.0f * (float)myFaceAngle);
    }
}
