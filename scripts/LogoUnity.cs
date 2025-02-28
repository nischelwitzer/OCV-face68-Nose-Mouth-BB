using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.UnityUtils.Helper;

[RequireComponent(typeof(Renderer))]

public class LogoUnity : MonoBehaviour
{
    private Renderer drawRenderer;
    private Texture2D drawTexture;
    private Mat drawMatrix;

    private int width  = 500;
    private int height = 500;

    public int doRotateX = 30;
    public int doRotateY = 40;
    public int doRotateZ = 50;



    void Start()
    {
        drawTexture = new Texture2D(width, height, TextureFormat.RGB24, false);

        drawRenderer = this.GetComponent<Renderer>();
        drawRenderer.material.mainTexture = drawTexture;

        drawCV();
    }

    private void Update()
    {
        this.transform.Rotate(doRotateX*Time.deltaTime, doRotateY * Time.deltaTime, doRotateZ * Time.deltaTime);
    }

    void drawCV()
    {
        drawMatrix = new Mat(drawTexture.height, drawTexture.width, CvType.CV_8UC4);
        int width = drawTexture.width;
        int height = drawTexture.height;

        // Hintergrund weiﬂ
        Imgproc.rectangle(drawMatrix, new Point(0, 0), new Point(width, height), new Scalar(255, 255, 255), -1);

        // Text
        Point bottomCenterPoint = new Point(width / 3, height - 30);
        Imgproc.putText(drawMatrix, "Unity", bottomCenterPoint, Imgproc.FONT_HERSHEY_SIMPLEX, 2, new Scalar(0, 0, 0), 1, Imgproc.LINE_AA, false);
        
        
        // Linke Seite
        List<Point> unityLogoPoints = new List<Point>();
        unityLogoPoints.Add(new Point(45,240));
        unityLogoPoints.Add(new Point(157,128));
        unityLogoPoints.Add(new Point(210,128));
        unityLogoPoints.Add(new Point(122, 220));

        unityLogoPoints.Add(new Point(122, 260));
        unityLogoPoints.Add(new Point(210, 350));
        unityLogoPoints.Add(new Point(160, 350));
        unityLogoPoints.Add(new Point(45, 240));

        MatOfPoint unityLogoPointsMat = new MatOfPoint();
        unityLogoPointsMat.fromList(unityLogoPoints);

        List<MatOfPoint> unityLogoContours = new List<MatOfPoint>();
        unityLogoContours.Add(unityLogoPointsMat);

        Imgproc.fillPoly(drawMatrix, unityLogoContours, new Scalar(0, 0, 0));

        // Rechte Seite
        List<Point> unityLogoPoints2 = new List<Point>();
        unityLogoPoints2.Add(new Point(204, 127));
        unityLogoPoints2.Add(new Point(226, 88));
        unityLogoPoints2.Add(new Point(380, 45));
        unityLogoPoints2.Add(new Point(420, 200));

        unityLogoPoints2.Add(new Point(400, 240));
        unityLogoPoints2.Add(new Point(420, 280));
        unityLogoPoints2.Add(new Point(380, 430));
        unityLogoPoints2.Add(new Point(230, 390));
        unityLogoPoints2.Add(new Point(200, 350));

        unityLogoPoints2.Add(new Point(210, 346));
        unityLogoPoints2.Add(new Point(326, 377));
        unityLogoPoints2.Add(new Point(360, 360));
        unityLogoPoints2.Add(new Point(390, 240));

        unityLogoPoints2.Add(new Point(360, 120));
        unityLogoPoints2.Add(new Point(320, 100));
        unityLogoPoints2.Add(new Point(208, 130));


        MatOfPoint unityLogoPoints2Mat = new MatOfPoint();
        unityLogoPoints2Mat.fromList(unityLogoPoints2);

        List<MatOfPoint> unityLogoContours2 = new List<MatOfPoint>();
        unityLogoContours2.Add(unityLogoPoints2Mat);

        Imgproc.fillPoly(drawMatrix, unityLogoContours2, new Scalar(0, 0, 0));

        // Mittelteil
        List<Point> unityLogoPoints3 = new List<Point>();
        unityLogoPoints3.Add(new Point(120, 220));
        unityLogoPoints3.Add(new Point(260, 220));
        unityLogoPoints3.Add(new Point(320, 100));
        unityLogoPoints3.Add(new Point(360, 120));

        unityLogoPoints3.Add(new Point(300, 240));
        unityLogoPoints3.Add(new Point(363, 360));
        unityLogoPoints3.Add(new Point(327, 380));
        unityLogoPoints3.Add(new Point(260, 260));
        unityLogoPoints3.Add(new Point(120, 260));

        MatOfPoint unityLogoPoints3Mat = new MatOfPoint();
        unityLogoPoints3Mat.fromList(unityLogoPoints3);

        List<MatOfPoint> unityLogoContours3 = new List<MatOfPoint>();
        unityLogoContours3.Add(unityLogoPoints3Mat);

        Imgproc.fillPoly(drawMatrix, unityLogoContours3, new Scalar(0, 0, 0));


        ////////////////////

        OpenCVForUnity.UnityUtils.Utils.matToTexture2D(drawMatrix, drawTexture);
    }


}
