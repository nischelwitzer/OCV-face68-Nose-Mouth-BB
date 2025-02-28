using UnityEngine;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;

[RequireComponent(typeof(Renderer))]

public class CubeDraw : MonoBehaviour
{
    private Renderer drawRenderer;
    private Texture2D drawTexture;
    private Mat drawMatrix;

    private int width = 500;
    private int height = 500;

    public int doRotateX = 30;
    public int doRotateY = 40;
    public int doRotateZ = 50;

    void Start()
    {
        drawTexture = new Texture2D(width, height, TextureFormat.RGB24, false);

        drawRenderer = this.GetComponent<Renderer>();
        drawRenderer.material.mainTexture = drawTexture;

        drawMatrix = new Mat(drawTexture.height, drawTexture.width, CvType.CV_8UC3);

        drawCV();
    }

    private void Update()
    {
        this.transform.Rotate(doRotateX * Time.deltaTime, doRotateY * Time.deltaTime, doRotateZ * Time.deltaTime);
    }

    private void drawCV()
    {
        Point centerPoint = new Point(width / 2, height / 2);

        Imgproc.line(drawMatrix, new Point(width-1, 0), new Point(0, height - 1), new Scalar(0, 255, 0), 2);
        Imgproc.line(drawMatrix, new Point(0, 0), new Point(width - 1, height - 1), new Scalar(0, 255, 0), 2);
        Imgproc.circle(drawMatrix, centerPoint, 200, new Scalar(255, 0, 0), 5);
        Imgproc.circle(drawMatrix, centerPoint, 10, new Scalar(255, 255, 0), -1);

        Imgproc.rectangle(drawMatrix, centerPoint + new Point(200,200), centerPoint - new Point(200, 200),
                new Scalar(0, 255, 255),2, Imgproc.LINE_8, 0);

        Imgproc.putText(drawMatrix, "OpenCV", centerPoint - new Point(80, 120), 
            Imgproc.FONT_HERSHEY_SIMPLEX, 1.2, new Scalar(255, 255, 255), 1, Imgproc.LINE_AA, false);
        Imgproc.putText(drawMatrix, "@Unity", centerPoint - new Point(70, -150),
            Imgproc.FONT_HERSHEY_PLAIN, 2.1, new Scalar(255, 255, 255), 2, Imgproc.LINE_AA, false);

        Imgproc.ellipse(drawMatrix, centerPoint, new Size(width / 4, height / 16),
                45.0, 0.0, 360.0, new Scalar(255, 0, 255), 3, Imgproc.LINE_4);
        Imgproc.ellipse(drawMatrix, centerPoint, new Size(width / 4, height / 16),
                -45.0, 0.0, 360.0, new Scalar(255, 0, 255), 3, Imgproc.LINE_4);

        OpenCVForUnity.UnityUtils.Utils.matToTexture2D(drawMatrix, drawTexture);
    }
}
