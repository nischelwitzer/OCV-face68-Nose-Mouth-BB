using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceBBoxStatus : MonoBehaviour
{
    void Update()
    {
        List<UnityEngine.Rect> faceBoundingBox = DMT.StaticStore.myFaceRects;

        if (faceBoundingBox != null)
            foreach (UnityEngine.Rect rect in faceBoundingBox)
            {
                Debug.Log("faceBoundingBox : X " + rect.xMin + "-" + rect.xMax + " Y " + rect.yMin + "-" + rect.yMax);
            }
    }
}