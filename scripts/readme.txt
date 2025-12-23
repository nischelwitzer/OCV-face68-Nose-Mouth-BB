

## General
```
                // ##########################################################################
                // ## MAIN Face Detection 
                // ##########################################################################

                /* points overview:
                    0...16  LeftEar-Chin-RightEar
                    17...21 LeftEyeBrowLR
                    22...26 RightEyeBrowLR
                    27...30 NoseTop-NoseTip
                    31...35 NoseBottomLR

                    36...39 LeftEyeTopLR
                    40...41 LeftEyeBottomRL
                    42...45 RightEyeTopLR
                    46...47 RightEyeBottomRL

                    48...54 OuterMouthTopLR
                    55...59 OuterMouthBottomRL
                    60...64 InnerMouthTopLR
                    65...67 InnerMouthTopRL
                */

                // ################################################################
                // DETECTION: detect face rects
                List<UnityEngine.Rect> detectResult = faceLandmarkDetector.Detect();

                // ################################################################

                DMT.StaticStore.myFaceRects = detectResult;      // all Results
                DMT.StaticStore.myFaceCnt = detectResult.Count;  // detected faces


                foreach (var rect in detectResult)
                {
                    int run = 0;

                    //detect landmark points
                    List<Vector2> points = faceLandmarkDetector.DetectLandmark(rect);
                    DMT.StaticStore.myFaceList = points;

                    //draw landmark points
                    // OpenCVForUnityUtils.DrawFaceLandmark(rgbMat, points, new Scalar(0, 255, 0), 2);

                    //draw face rect
                    // OpenCVForUnityUtils.DrawFaceRect(rgbMat, rect, new Scalar(255, 0, 0), 2);
                    // my boundingbox cross
                    // Imgproc.line(rgbMat, new Point(rect.xMax, rect.yMax), new Point(rect.xMin, rect.yMin), new Scalar(0, 255, 0), 2);
                    // Imgproc.line(rgbMat, new Point(rect.xMax, rect.yMin), new Point(rect.xMin, rect.yMax), new Scalar(0, 255, 0), 2);

```
## BoundingBox
```

                    // ------------------------------------------------------------------
                    // my OWN face boundingbox cross - BLUE
                    // nicht für Neigung
                    // Point faceBBMin = new Point(points[0].x, points[19].y);
                    // Point faceBBMax = new Point(points[16].x, points[8].y);

                    Point faceBBMin = new Point(999, 999);
                    Point faceBBMax = new Point(0, 0);
                    for (run = 0; run < points.Count; run++) // check all Points
                    {
                        if (points[run].x > faceBBMax.x) faceBBMax.x = points[run].x;
                        if (points[run].y > faceBBMax.y) faceBBMax.y = points[run].y;
                        if (points[run].x < faceBBMin.x) faceBBMin.x = points[run].x;
                        if (points[run].y < faceBBMin.y) faceBBMin.y = points[run].y;
                    }
                    DMT.StaticStore.myBoundingBox =
                        new UnityEngine.Rect((float)faceBBMin.x, (float)faceBBMin.y, (float)faceBBMax.x - (float)faceBBMin.x, (float)faceBBMax.y - (float)faceBBMin.y);

                    Imgproc.rectangle(rgbMat, faceBBMin, faceBBMax, new Scalar(0, 128, 0), 2);

                    Imgproc.line(rgbMat, new Point(faceBBMin.x, faceBBMin.y), new Point(faceBBMax.x, faceBBMax.y), new Scalar(0, 0, 0), 1);
                    Imgproc.line(rgbMat, new Point(faceBBMax.x, faceBBMin.y), new Point(faceBBMin.x, faceBBMax.y), new Scalar(0, 0, 0), 1);

```
## Mund
```

                    // **************************************************************
                    // Mund-Distanz open/close

                    float mouthDistanz = Vector2.Distance(points[62], points[66]); // 51-57 62-66
                    Imgproc.line(rgbMat, new Point(points[62].x, points[62].y),
                                         new Point(points[66].x, points[66].y), new Scalar(255, 255, 0), 5);

                    DMT.StaticStore.MouthOpen = mouthDistanz;

                    double blueHeight = faceBBMax.y - faceBBMin.y;
                    double mouthNDC = mouthDistanz / blueHeight;

                    String mouthTxt = "Mouth O/C: " + mouthNDC.ToString("0.00");
                    Imgproc.putText(rgbMat, mouthTxt, new Point(points[62].x+10, (points[62].y+ points[66].y)/2+10),
                        Imgproc.FONT_HERSHEY_PLAIN, 1.4, new Scalar(255, 255, 0), 1, Imgproc.LINE_4, false);

```
## Kopf Neigung
```

                    // **************************************************************
                    // Kopfbewegung left/right

                    float leftDistanz = Vector2.Distance(points[2], points[30]);
                    float rightDistanz = Vector2.Distance(points[30], points[14]);

                    Imgproc.line(rgbMat, new Point(points[2].x, points[2].y),
                                         new Point(points[30].x, points[30].y), new Scalar(255, 0, 0), 3);
                    Imgproc.line(rgbMat, new Point(points[30].x, points[30].y),
                                         new Point(points[14].x, points[14].y), new Scalar(0, 0, 255), 3);

                    float compareDistanz = leftDistanz / (leftDistanz+rightDistanz);

                    String headTxt = "Compare L/R: " + compareDistanz.ToString("0.00");
                    Imgproc.putText(rgbMat, headTxt, new Point(faceBBMin.x, faceBBMin.y) + new Point(0, -10),
                        Imgproc.FONT_HERSHEY_PLAIN, 1.2, new Scalar(0, 0, 255), 1, Imgproc.LINE_4, false);

                    // **************************************************************
                    // Winkel Head

                    Point centerPoint = new Point(points[8].x, points[8].y);
                    Point winkelPoint = new Point(points[27].x, points[27].y);

                    Imgproc.line(rgbMat, centerPoint, winkelPoint, new Scalar(255, 128, 128), 2);

                    double faceAngle = Math.Atan2(winkelPoint.y - centerPoint.y,
                            winkelPoint.x - centerPoint.x) * 180.0f / Math.PI + 90.0f;

                    DMT.StaticStore.FaceAngle = faceAngle;

                    String angleTxt = "Rotate: " + faceAngle.ToString("0.00");
                    Imgproc.putText(rgbMat, angleTxt, new Point(faceBBMin.x, faceBBMax.y) + new Point(0, +15), 
                        Imgproc.FONT_HERSHEY_PLAIN, 1.2,
                        new Scalar(255, 64, 64), 1, Imgproc.LINE_4, false);
```
## Nase
```

                    // **************************************************************
                    // my Nose Point
                    // relative Änderung über "noseThick"

                    Point nosePoint = new Point(points[30].x, points[30].y);
                    int noseThick = Convert.ToInt32((faceBBMax.x - faceBBMin.x) * 0.05);
                    Imgproc.circle(rgbMat, nosePoint, noseThick, new Scalar(255, 0, 0), -1);

                    // Text Info Pixel
                    Imgproc.putText(rgbMat, "Px: "+ points[30].x+" "+points[30].y, nosePoint, 
                        Imgproc.FONT_HERSHEY_SIMPLEX, 0.5, new Scalar(255, 255, 0), 1, Imgproc.LINE_AA, false);

                    // Text Info NDX 
                    float noseX = points[30].x / rgbMat.width();
                    float noseY = points[30].y / rgbMat.height();
                    String noseNDC = "NDC: " + (noseX * 100).ToString("000") + "% " 
                                             + (noseY * 100).ToString("000") + "% "; ;
                    Imgproc.putText(rgbMat, noseNDC, new Point(nosePoint.x, nosePoint.y + 15), 
                        Imgproc.FONT_HERSHEY_SIMPLEX, 0.5, new Scalar(255, 255, 0), 1, Imgproc.LINE_AA, false);

```
## Zeichnen
```

                    // ***************************************************************************************
                    // NIS drawing
                    // draw feature points

                    Mat matFaceNumber = new Mat(debugTexture.height, debugTexture.width, CvType.CV_8UC4);

                    Point oldPoint = new Point(0, 0);
                    Point newPoint = new Point(0, 0);

                    for (run = 0; run < points.Count; run++) // lines
                    {
                        newPoint = new Point(points[run].x, points[run].y);
                        // Debug.Log("FacePoints: " + run + " -> " + newPoint); // normal 68 points

                        if ((run != 0) && (run != 17) && (run != 22) && (run != 27) && (run != 36) && (run != 42) && (run != 48))
                        {
                            // OpenCVForUnity.ImgprocModule.Imgproc
                            Imgproc.line(matFaceNumber, oldPoint, newPoint, new Scalar(255, 255, 0), 2);
                        }
                        oldPoint = newPoint;
                    }

                    // my Nose Point
                    // Point nosePoint = new Point(points[30].x, points[30].y);
                    // Imgproc.circle(rgbMat, nosePoint, 2, new Scalar(255, 0, 0), 30);

                    for (run = 0; run < points.Count; run++) // points and text
                    {
                        newPoint = new Point(points[run].x, points[run].y);
                        // NIS 
                        Imgproc.circle(matFaceNumber, newPoint, 2, new Scalar(255, 0, 0), 1);
                        Imgproc.circle(rgbMat, newPoint, 2, new Scalar(255, 0, 0), -1);
                        Imgproc.putText(matFaceNumber, run.ToString(), newPoint, Imgproc.FONT_HERSHEY_SIMPLEX, 0.3, new Scalar(255, 255, 255), 1, Imgproc.LINE_AA, false);
                        oldPoint = newPoint;
                    }

                    // OpenCVForUnity.UnityUtils.Utils.texture2DToMat(textureWork, matFaceNumber);
                    Imgproc.putText(matFaceNumber, "DebugView", new Point(10, 50), Imgproc.FONT_HERSHEY_SIMPLEX, 1.5, new Scalar(255, 255, 255), 2, Imgproc.LINE_AA, false);
                    Imgproc.rectangle(matFaceNumber, new Point(10, 10), new Point(imgWidth - 10, imgHeight - 10), new Scalar(0, 200, 0), 5);
                    Imgproc.line(matFaceNumber, new Point(50, 100), new Point(imgWidth - 50, 100), new Scalar(255, 255, 0), 5);
                    Imgproc.circle(matFaceNumber, new Point(imgWidth / 2, imgHeight / 2), (imgHeight - 50) / 2, new Scalar(200, 0, 200), 4);
                    OpenCVForUnity.UnityUtils.Utils.matToTexture2D(matFaceNumber, debugTexture);

                    // drawing over
                    // ***************************************************************************************


                }

```
## more Drawing
```

                Imgproc.putText(rgbMat, "W:" + rgbMat.width() + " H:" + rgbMat.height() + " SO:" + Screen.orientation, new Point(10, rgbMat.rows() - 100), Imgproc.FONT_HERSHEY_SIMPLEX, 0.5, new Scalar(255, 255, 255, 255), 1, Imgproc.LINE_AA, false);
                Imgproc.putText(rgbMat, "MainView", new Point(10, 350), Imgproc.FONT_HERSHEY_SIMPLEX, 0.8, new Scalar(255, 255, 255), 2, Imgproc.LINE_AA, false);

                // OpenCV Drawing Example
                // DRAWING

                Mat infoMatrix = new Mat(infoTexture.height, infoTexture.width, CvType.CV_8UC1);
                Imgproc.cvtColor(rgbMat, infoMatrix, Imgproc.COLOR_RGB2GRAY);
                Imgproc.putText(infoMatrix, "InfoView", new Point(20, 50), Imgproc.FONT_HERSHEY_SIMPLEX, 1.5, new Scalar(255, 255, 255), 4, Imgproc.LINE_AA, false);
                // Core.flip(infoMatrix, infoMatrix, 0);
                OpenCVForUnity.UnityUtils.Utils.matToTexture2D(infoMatrix, infoTexture);

                /*
                Mat infoMatrix = new Mat(infoTexture.height, infoTexture.width, CvType.CV_8UC4); // CV_8UC4 RGBA32 
                Imgproc.arrowedLine(infoMatrix, new Point(100, 500), new Point(500, 100), new Scalar(255, 255, 0), 4, Imgproc.LINE_8, 0, 0.1);
                Imgproc.putText(infoMatrix, "InfoView", new Point(20, 50), Imgproc.FONT_HERSHEY_SIMPLEX, 1.5, new Scalar(255, 255, 255), 4, Imgproc.LINE_AA, false);
                OpenCVForUnity.UnityUtils.Utils.matToTexture2D(infoMatrix, infoTexture);
                */

                OpenCVForUnity.UnityUtils.Utils.fastMatToTexture2D(rgbMat, mainTexture);
                // OpenCVForUnity.UnityUtils.Utils.fastMatToTexture2D(rgbaMat, infoTexture);
                // OpenCVForUnity.UnityUtils.Utils.fastMatToTexture2D(rgbaMat, debugTexture);
                // OpenCVForUnity.UnityUtils.Utils.fastMatToTexture2D(rgbaMat, debugTexture); // draw face

                // -----------------
            }

            if (Input.GetKeyDown("m")) // toggel fps monitor
            {
                this.GetComponent<FpsMonitor>().enabled = !this.GetComponent<FpsMonitor>().enabled;
            }
        }
```
