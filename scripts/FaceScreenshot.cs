using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// or in openCV
// savePath = Application.persistentDataPath + "/matrix.jpg";
// Imgcodecs.imwrite(savePath, cameraMat);

public class FaceScreenshot : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            StartCoroutine(TakeScreenShot());
        }
    }

    IEnumerator TakeScreenShot()
    {
        yield return new WaitForEndOfFrame();
        string filename = "face_screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        ScreenCapture.CaptureScreenshot(filename, 2);
        Debug.Log("Face Screenshot taken!");
    }
}

