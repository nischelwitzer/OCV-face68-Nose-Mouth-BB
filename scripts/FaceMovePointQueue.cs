using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// last changed 16.04.2024 
// Alexander Nischelwitzer, FH JOANNEUM, Wirtschaftsinformatik

// FIFO
// see https://de.wikipedia.org/wiki/First_In_%E2%80%93_First_Out
// see https://www.geeksforgeeks.org/c-sharp-queue-with-examples/

public class FaceMovePointQueue : MonoBehaviour
{
    public bool showDebugging = false;
    public bool useMean = false;
    public bool useMedian = false;
    public int valSize = 7; // calculation window size

    private float myWidth = 0;
    private float myHeight = 0;

    private Queue pointQueue = new Queue();

    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas"); // FindObjectOfType<Canvas>();
        myHeight = canvas.GetComponent<RectTransform>().rect.height;
        myWidth = canvas.GetComponent<RectTransform>().rect.width;
        Debug.Log("Canvas Info: " + myWidth + " " + myHeight);


        Vector2 centerPoint = new Vector2(0.5f, 0.5f);

        byte run = 0;
        for (run = 0; run < valSize; run++)
            pointQueue.Enqueue(centerPoint);

        Debug.Log("CanvasSize: " + myWidth + " " + myHeight);
    }

    void Update()
    {

        float xPoint = +1.5f * (DMT.StaticStore.leftRightFace - 0.5f) * myWidth;
        float yPoint = -4.0f * (DMT.StaticStore.upDownFace - 0.5f) * myHeight;
        Vector2 myPoint = new Vector2(xPoint, yPoint);

        // exchange Element

        pointQueue.Enqueue(myPoint);  // first IN  FIFO
        pointQueue.Dequeue();         // last  OUT FIFO

        // calc Mean/Median

        Vector2 meanPoint = getMeanPoint(pointQueue);
        Vector2 medianPoint = getMedianPoint(pointQueue);

        // show

        RectTransform rt = this.GetComponent<RectTransform>();
        if (useMean) rt.transform.localPosition = new Vector3(meanPoint.x, meanPoint.y, 0);
        else
        if (useMedian)
            rt.transform.localPosition = new Vector3(medianPoint.x, medianPoint.y, 0);
        else
            rt.transform.localPosition = new Vector3(xPoint, yPoint, 0);

        if (showDebugging)
        { 
            Debug.Log("StatisticCalc: SIZE> " + pointQueue.Count.ToString("00") + " MEAN> "+meanPoint.ToString("000.00") + " MEDIAN> " + medianPoint.ToString("000.00"));
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
        return resPoint/calcPoints.Count;
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

        resPoint = new Vector2(calcX[(int)Mathf.Round((calcX.Length - 1) / 2)],
                           calcY[(int)Mathf.Round((calcY.Length - 1) / 2)]);
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

}
