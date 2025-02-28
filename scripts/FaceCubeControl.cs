using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FaceCubeControl : MonoBehaviour
{
    public bool moveAbs = true;
    public int moveVelocity = 10;

    public Material materialGreen;
    public Material materialRed;

    public TMP_Text myText;
    private const int noseIndex = 30;

    private const int mouthIndex1 = 51;
    private const int mouthIndex2 = 57;

    private Vector2 refPoint = Vector2.zero;

    void Start()
    {
        // myText = GameObject.Find("StatusInfoRight").GetComponent<TMP_Text>();
    }

    void Update()
    {
        List<UnityEngine.Rect> myRList = DMT.StaticStore.myFaceRects;
        List<Vector2> myFList = DMT.StaticStore.myFaceList;

        int numFaces = DMT.StaticStore.myFaceCnt;
        if (numFaces > 0)
            this.GetComponent<Renderer>().material = materialGreen;
        else
            this.GetComponent<Renderer>().material = materialRed;
        if (myText != null)
            myText.text = "Faces: " + numFaces + "\n";

        if ((myRList != null) && (myRList.Count > 0)) // if one face - go on
        {
            if (Input.GetKey("space")) // store reference Point (NOSE 30)
            {
                refPoint = myFList[noseIndex];
            }

            Vector2 bbCenter;
            bbCenter.x = (myRList[0].xMax + myRList[0].xMin) / 2;
            bbCenter.y = (myRList[0].yMax + myRList[0].yMin) / 2;

            Vector2 bbCenterNDC;
            bbCenterNDC.x = bbCenter.x / DMT.StaticStore.imgWidth;
            bbCenterNDC.y = bbCenter.y / DMT.StaticStore.imgHeight;

            Vector2 sizeImg = new Vector2(DMT.StaticStore.imgWidth, DMT.StaticStore.imgHeight);

            Vector2 nosePoint = myFList[noseIndex];
            Vector2 nosePointNDC = nosePoint / sizeImg;

            float mouthDistanz = Vector2.Distance(myFList[mouthIndex1], myFList[mouthIndex2]);

            if (refPoint != Vector2.zero)
            {
                float deltaX = refPoint.x - nosePoint.x;
                float deltaY = refPoint.y - nosePoint.y;

                if (moveAbs)
                    this.transform.rotation = Quaternion.Euler(deltaY, deltaX, 0); // abs
                else
                    this.transform.Rotate(deltaY / 100 * moveVelocity, deltaX / 100 * moveVelocity, 0); // rel, inc
            }

            myText.text += "BB Center: " + bbCenter.ToString("000") + "\n";
            myText.text += "BB CenterNDC: " + bbCenterNDC + "\n";
            myText.text += "Nose [" + noseIndex + "]: " + nosePoint.ToString("000") + "\n";
            myText.text += "NoseNDC [" + noseIndex + "]: " + nosePointNDC + "\n";
            myText.text += "Mouth Distance: " + mouthDistanz.ToString("000") + "\n";

            this.transform.localScale = new Vector3(mouthDistanz * 4, mouthDistanz * 4, mouthDistanz * 4);
        }

    }
}
