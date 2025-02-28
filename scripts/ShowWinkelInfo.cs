using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowWinkelInfo : MonoBehaviour
{
    public TMP_Text myText;

    void Update()
    {
       double myFaceAngle = DMT.StaticStore.FaceAngle;

        if (myText != null)
            myText.text = "Winkel: " + myFaceAngle.ToString("0.00") + "°"; 
    }
}
