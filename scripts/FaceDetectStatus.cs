using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDetectStatus : MonoBehaviour
{
    void Update()
    {
        int numFaces = DMT.StaticStore.myFaceCnt;

        if (numFaces > 0)
            this.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        else 
            this.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
    }
}
