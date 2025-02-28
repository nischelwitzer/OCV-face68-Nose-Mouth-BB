using OpenCVForUnity.UnityUtils.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera Live Switcher with Number 0..4 (five Cams) and Storage
/// AK Nischelwitzer, FH JOANNEUM, IMA, DMT, 05/2023
/// Switch OpenCV LiveWebcam
/// Stores current Cam in PlayerPrefs 
/// </summary>

[RequireComponent(typeof(WebCamTextureToMatHelper))]

public class CamSwitchStore : MonoBehaviour
{
    private string startCam = "0";
    private string newCam = "0";

    public bool usePrefab = true;
    public string defaultCam = "0";

    void Start()
    {
        if (usePrefab)
        {
            startCam = PlayerPrefs.GetString("startCam", "0");
            newCam = startCam;
        }
        else
        {
            startCam = defaultCam;
            newCam = defaultCam;
        }

        this.GetComponent<WebCamTextureToMatHelper>().requestedDeviceName = startCam;
        Debug.Log("Setting Camera (playerpref StartCam) to: " + startCam);
    }

    void Update()
    {
        newCam = "same";

        if (Input.GetKeyDown("0")) newCam = "0";
        if (Input.GetKeyDown("a")) newCam = "1";
        if (Input.GetKeyDown("b")) newCam = "2";

        if (Input.GetKeyDown("r")) // refresh
        {
            PlayerPrefs.GetString("startCam", startCam);
            this.GetComponent<WebCamTextureToMatHelper>().requestedDeviceName = startCam;
            this.GetComponent<WebCamTextureToMatHelper>().Initialize();
            Debug.Log("Refresh Camera: " + newCam);
        }

        if (newCam != "same") // cam change request
        {
            if (newCam != startCam) // new camera request
            {
                PlayerPrefs.SetString("startCam", newCam);
                this.GetComponent<WebCamTextureToMatHelper>().requestedDeviceName = newCam;
                this.GetComponent<WebCamTextureToMatHelper>().Initialize();
                Debug.Log("Setting NEW Camera (playerpref startCam) to: " + newCam);
                startCam = newCam;
            }
            else // request for the same camera
            {
                Debug.Log("No change! Camera already active: " + newCam);
            }
        }
    }
}
