using OpenCVForUnity.UnityUtils.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class AwakeCamera : MonoBehaviour
{
    void Start()
    {
        GameObject myCamGO = GameObject.Find("Face68_detection");

        if (myCamGO != null)
        {
            string startCam = "";
            PlayerPrefs.GetString("startCam", startCam);
            myCamGO.GetComponent<WebCamTextureToMatHelper>().requestedDeviceName = startCam;
            myCamGO.GetComponent<WebCamTextureToMatHelper>().Initialize();
            Debug.Log("Awake/Refresh Camera: " + startCam);
        }
    }
}
