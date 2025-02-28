using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.UnityUtils.Helper;

[RequireComponent(typeof(Renderer))]

public class LogoOpenCV : MonoBehaviour
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

        // Hintergrund weiß
        Imgproc.rectangle(drawMatrix, new Point(0, 0), new Point(width, height), new Scalar(255, 255, 255), -1);

        // Text
        Point bottomCenterPoint = new Point(width / 7, height - 30);
        Imgproc.putText(drawMatrix, "OpenCV", bottomCenterPoint, Imgproc.FONT_HERSHEY_SIMPLEX, 3, new Scalar(0, 0, 0), 1, Imgproc.LINE_AA, false);


        // Roter Kreis
        Point redCircleCenterPoint = new Point(width / 2, 110);
        Imgproc.ellipse(drawMatrix, redCircleCenterPoint, new Size(90, 90), 120, 0, 300, new Scalar(255, 0, 0), -1);
        // Weißer Kreis im Inneren des roten Kreises
        Imgproc.circle(drawMatrix, redCircleCenterPoint, 40, new Scalar(255, 255, 255), -1);


        // Grüner Kreis
        Point greenCircleCenterPoint = new Point(width / 3.3, 280);
        Imgproc.ellipse(drawMatrix, greenCircleCenterPoint, new Size(90, 90), 0, 0, 300, new Scalar(0, 255, 0), -1);
        // Weißer Kreis im Inneren des grünen Kreises
        Imgproc.circle(drawMatrix, greenCircleCenterPoint, 40, new Scalar(255, 255, 255), -1);


        // Blauer Kreis
        Point blueCircleCenterPoint = new Point((width / 3.3) + 200, 280);
        Imgproc.ellipse(drawMatrix, blueCircleCenterPoint, new Size(90, 90), -60, 0, 300, new Scalar(0, 0, 255), -1);
        // Weißer Kreis im Inneren des blauen Kreises                                                                                                       
        Imgproc.circle(drawMatrix, blueCircleCenterPoint, 40, new Scalar(255, 255, 255), -1);


        OpenCVForUnity.UnityUtils.Utils.matToTexture2D(drawMatrix, drawTexture);
    }


}
