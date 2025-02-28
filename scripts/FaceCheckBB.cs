using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceCheckBB : MonoBehaviour
{

    public bool showDebugging = false;

    public int minBB = 11;
    public int maxBB = 18;

    public int noseX = 50;
    public int noseY = 60;
    public int noseDeltaX = 10; // +/-% for Nose NDC Point 
    public int noseDeltaY = 7;


    void Update()
    {
        UnityEngine.Rect myBB = DMT.StaticStore.myBoundingBoxNDC;
        float bbSize = myBB.width * myBB.height * 100;
        if (showDebugging)
        {
            Debug.Log("BB-NDC: " + bbSize.ToString("000") + " " + myBB);
            Debug.Log("Nose-NDC:" + DMT.StaticStore.NoseNDC);
        }

        Vector2 myNoseNDC = DMT.StaticStore.NoseNDC * 100;

        bool okBB = false;
        if ((minBB <= bbSize) && (bbSize <= maxBB)) okBB = true;

        bool okNose = false;
        if (((noseX - noseDeltaX) <= myNoseNDC.x) && (myNoseNDC.x <= (noseX + noseDeltaX)) &&
             ((noseY - noseDeltaY) <= myNoseNDC.y) && (myNoseNDC.y <= (noseY + noseDeltaY))) okNose = true;


        if (okBB && okNose)
            this.GetComponent<Image>().color = new Color32(0, 255, 0, 100); // GREEN
        else
        {
            if (okBB)
                this.GetComponent<Image>().color = new Color32(0, 0, 255, 100); // BLUE only BB
            else
                this.GetComponent<Image>().color = new Color32(255, 0, 0, 100); // RED no BB and Nose
        }
    }
}

