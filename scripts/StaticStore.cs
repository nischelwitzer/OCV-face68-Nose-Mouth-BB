using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// usage: DMT.StaticStore.myData = ...

namespace DMT
{
    public static class StaticStore
    {

        private static int _myFaceCnt = 0;
        private static bool _camInit = false;
        private static List<Vector2> _faceList = null;
        private static List<UnityEngine.Rect> _faceRectList = null;
        private static UnityEngine.Rect _boundingBox = Rect.zero;
        private static UnityEngine.Rect _boundingBoxNDC = Rect.zero;
        private static double _faceAngle = 0;

        public const int imgWidth = 640;
        public const int imgHeight = 480;

        public static double MouthOpen { get; set; }
        public static float leftRightFace { get; set; }
        public static float upDownFace { get; set; }
        public static Vector2 NoseNDC { get; set; }

        // public static double FaceAngle { get; set; } // face angle (Winkel)

        public static double FaceAngle 
        {
           get { return _faceAngle; }
           set { _faceAngle = value; }
        }

        public static int myFaceCnt // face counter (Anzahl)
        {
            get { return _myFaceCnt; }

            set
            {
                int gotData = value;
                if ((gotData >= 0) && (gotData <= 10))
                {
                    _myFaceCnt = value;
                    // UnityEngine.Debug.Log("StaticStore FaceCounter: " + _myFaceCnt);
                }
                else
                {
                    UnityEngine.Debug.LogWarning("setter warning for DMT.StaticStore _myFaceCnt - got: " + gotData);
                }
            }
        }

        public static List<Vector2> myFaceList // face Points
        {
            get { return _faceList; }
            set
            {
                List<Vector2> gotData = value;
                _faceList = value;
            }
        }

        public static List<UnityEngine.Rect> myFaceRects // faceRect - BoundingBoxes
        {
            get { return _faceRectList; }
            set
            {
                List<UnityEngine.Rect> gotData = value;
                _faceRectList = value;
            }
        }

        public static UnityEngine.Rect myBoundingBox // main BoundingBoxes
        {
            get { return _boundingBox; }
            set
            {
                UnityEngine.Rect gotData = value;
                _boundingBox = gotData;
            }
        }

        public static UnityEngine.Rect myBoundingBoxNDC // main BoundingBoxes
        {
            get { return _boundingBoxNDC; }
            set
            {
                UnityEngine.Rect gotData = value;
                _boundingBoxNDC = gotData;
            }
        }

        public static bool camInit
        {
            get { return _camInit; }
            set { _camInit = value; }
        }

    }
}
