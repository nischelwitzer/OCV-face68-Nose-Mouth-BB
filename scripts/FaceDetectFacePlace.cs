using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceDetectFacePlace : MonoBehaviour
{
    void Update()
    {
        int numFaces = DMT.StaticStore.myFaceCnt;

        if (numFaces > 0)
            this.GetComponent<Image>().color = new Color32(0, 255, 0, 100); // GREEN
        else
            this.GetComponent<Image>().color = new Color32(255, 0, 0, 100); // RED
    }
}
