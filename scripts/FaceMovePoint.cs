using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMovePoint : MonoBehaviour
{
    public bool showDebugging = false;
    public bool useMean = false;
    public bool useMedian = false;

    private float myWidth = 800;
    private float myHeight = 600;

    private float[] valRawX;
    private float[] valRawY;
    private int valSize = 7;

    private Queue pointQueue = new Queue();

    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas"); // FindObjectOfType<Canvas>();
        myHeight = canvas.GetComponent<RectTransform>().rect.height;
        myWidth = canvas.GetComponent<RectTransform>().rect.width;
        Debug.Log("Canvas Info: " + myWidth + " " + myHeight);

        valRawX = new float[valSize];
        valRawY = new float[valSize];

        Vector2 centerPoint = new Vector2(0.5f, 0.5f);

        byte run = 0;
        for (run = 0; run < valSize; run++)
            pointQueue.Enqueue(centerPoint);

        Debug.Log("CanvasSize: " + myWidth + " " + myHeight);
    }

    void Update()
    {
        // Camera.main.pixelWidth Screen.width
        // Camera.main.pixelHeight Screen.height

        float xPoint = +1.5f * (DMT.StaticStore.leftRightFace - 0.5f) * myWidth;
        float yPoint = -4.0f * (DMT.StaticStore.upDownFace - 0.5f) * myHeight;
        Vector2 myPoint = new Vector2(xPoint, yPoint);

        // exchange Element

        pointQueue.Enqueue(myPoint);
        pointQueue.Dequeue();

        valRawX = exElementX(valRawX, xPoint);
        valRawY = exElementY(valRawY, yPoint);

        // calc Mean/Median

        Vector2 meanPoint = getMeanPoint(pointQueue);
        Vector2 medianPoint = getMedianPoint(pointQueue);

        float meanX = getMean(valRawX);
        float meanY = getMean(valRawY);
        float medianX = getMedian(valRawX);
        float medianY = getMedian(valRawY);

        // show

        RectTransform rt = this.GetComponent<RectTransform>();
        if (useMean) rt.transform.localPosition = new Vector3(meanX, meanY, 0);
        else
        if (useMedian)
            rt.transform.localPosition = new Vector3(medianX, medianY, 0);
        else
            rt.transform.localPosition = new Vector3(xPoint, yPoint, 0);

        if (showDebugging)
        {
            Debug.Log("MeanCalc  :  A: " + meanX.ToString("000.00") + " " + meanY.ToString("000.00") + " <<>> B:" + meanPoint);
            Debug.Log("MedianCalc:  A: " + medianX.ToString("000.00") + " " + medianY.ToString("000.00") + " <<>> B:" + medianPoint);
            // Debug.Log("Mean/MedianCalc: Raw: " + myPoint + " X>" + xPoint + " ### Mean A: " + getMedian(valRawX) + " " + getMedian(valRawY) + " B:" + meanPoint);
            // showArray(valRawX, "X: ");
            // showArray(valRawY, "Y: ");
            // showQueue(pointQueue, "Q: ");
        }
    }

    // ###
    // ### Mean Median Calculation with FIFO Queue and Vector2/Point
    // ###

    Vector2 getMeanPoint(Queue calcPoints)
    {
        Vector2 resPoint = Vector2.zero;
        foreach (var item in calcPoints)
        {
            resPoint += (Vector2)item;
        }
        return resPoint / calcPoints.Count;
    }

    Vector2 getMedianPoint(Queue calcPoints)
    {
        Vector2 resPoint = Vector2.zero;

        float[] calcX = new float[calcPoints.Count];
        float[] calcY = new float[calcPoints.Count];

        byte run = 0;
        foreach (var item in calcPoints)
        {
            Vector2 pointItem = (Vector2)item;
            calcX[run] = pointItem.x;
            calcY[run] = pointItem.y;
            run++;
        }
        Array.Sort(calcX);
        Array.Sort(calcY);

        resPoint = new Vector2(calcX[ (int)Mathf.Round((calcX.Length - 1) / 2) ],
                           calcY[ (int)Mathf.Round((calcY.Length - 1) / 2) ]);
        return resPoint;
    }

    void showQueue(Queue calcPoints, string addText = "")
    {
        string outString = "";
        foreach (var item in calcPoints)
        {
            outString += item.ToString();
        }
        Debug.Log(addText + ": " + outString);
    }

    // ###
    // ### Mean Median Calculation with 2 Arrays
    // ###

    float getMean(float[] gotArray)
    {
        float sum = 0.0f;
        foreach (float item in gotArray)
            sum += item;
        return (sum / gotArray.Length);
    }

    float getMedian(float[] gotArray)
    {
        float[] copyRaw = (float[])gotArray.Clone();
        Array.Sort(copyRaw);
        return copyRaw[(int)Mathf.Round((copyRaw.Length - 1) / 2)];
    }

    float[] exElementX(float[] gotArray, float element)
    {
        for (int i = 0; i < gotArray.Length - 1; i++)
        { gotArray[i] = gotArray[i + 1]; }

        valRawX[gotArray.Length - 1] = element;
        return gotArray;
    }

    float[] exElementY(float[] gotArray, float element)
    {
        for (int i = 0; i < gotArray.Length - 1; i++)
        {
            gotArray[i] = gotArray[i + 1];
        }
        // Debug.Log("Element: " + element);

        valRawY[gotArray.Length - 1] = element;
        return gotArray;
    }

    void showArray(float[] outArray, string addText = "")
    {
        string outString = "";
        foreach (float item in outArray)
            outString += item.ToString("000.00") + " ";
        Debug.Log(addText + ": " + outString);
    }


}
