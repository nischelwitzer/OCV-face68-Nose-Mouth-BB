using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

// Developer can select which direction feedback will be shown
public enum FeedbackDirection { Nothing, Right, Left, Up, Down };

public class FeedbackBalken : MonoBehaviour
{
    // Image with ImageType filled
    public FeedbackDirection whichFeedback = FeedbackDirection.Nothing;

    public float upDownMultiplier = 3.0F;

    void Start()
    {
        DMT.StaticStore.leftRightFace = 0.5f;
        DMT.StaticStore.upDownFace = 0.5f;

        switch (whichFeedback)
        {
            case FeedbackDirection.Right:
                this.GetComponent<Image>().fillAmount = 0.0f;
                break;
            case FeedbackDirection.Left:
                this.GetComponent<Image>().fillAmount = 0.0f;
                break;
            case FeedbackDirection.Up:
                this.GetComponent<Image>().fillAmount = 0.0f;
                break;
            case FeedbackDirection.Down:
                this.GetComponent<Image>().fillAmount = 0.0f;
                break;
            default:
                Debug.Log("Nothing for BarFeedback selected!");
                break;
        }
    }

    void Update()
    {
        float myLeftRight = DMT.StaticStore.leftRightFace;
        float myUpDown = DMT.StaticStore.upDownFace;

        // Debug.Log("FedbackInfo: " + myUpDown);

        if ((whichFeedback == FeedbackDirection.Right) && (myLeftRight > 0.5))
            this.GetComponent<Image>().fillAmount = (myLeftRight - 0.5f) * 2.0f;

        if ((whichFeedback == FeedbackDirection.Left) && (myLeftRight < 0.5))
            this.GetComponent<Image>().fillAmount = (0.5f - myLeftRight) * 2.0f;

        if ((whichFeedback == FeedbackDirection.Up) && (myUpDown < 0.5))
            this.GetComponent<Image>().fillAmount = (0.5f - myUpDown) * 2.0f * upDownMultiplier;

        if ((whichFeedback == FeedbackDirection.Down) && (myUpDown > 0.5))
            this.GetComponent<Image>().fillAmount = (myUpDown - 0.5f) * 2.0f * upDownMultiplier;

    }
}
