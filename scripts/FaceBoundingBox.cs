using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FaceBoundingBox : MonoBehaviour
{
    public TMP_Text myText;

    void Update()
    {
        List<UnityEngine.Rect> myList = DMT.StaticStore.myFaceRects;
        UnityEngine.Rect myBB = DMT.StaticStore.myBoundingBox;

        if (myList != null)
            if (myList.Count > 0)
            {
                float bbArea = ((myList[0].xMax - myList[0].xMin) * (myList[0].yMax - myList[0].yMin))
                    / (DMT.StaticStore.imgWidth * DMT.StaticStore.imgHeight);

                float bbAreaDMT = ((myBB.xMax - myBB.xMin) * (myBB.yMax - myBB.yMin))
                    / (DMT.StaticStore.imgWidth * DMT.StaticStore.imgHeight);

                myText.text = "BB Area: " + (bbArea * 100).ToString("000") + "% \n";
                myText.text += "   Min/Max: " + myList[0].xMax.ToString("000") + "-" + myList[0].xMin.ToString("000") + "  " +
                                                myList[0].yMax.ToString("000") + "-" + myList[0].yMin.ToString("000") + " px \n";
                myText.text += "BB AreaDMT: " + (bbAreaDMT * 100).ToString("000") + "%\n";
                myText.text += "   Min/Max: " + myBB.xMax.ToString("000") + "-" + myBB.xMin.ToString("000") + "  " +
                                                myBB.yMax.ToString("000") + "-" + myBB.yMin.ToString("000") + " px \n";
            }
    }
}
