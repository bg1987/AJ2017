using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum LilyState
{
    Closed = 0,
    Green = 1,
    Purple = 2,
    Yellow = 3
}

public class LilypadsPetal : MonoBehaviour {

    [Header("Yellow Path")]
    public LilypadsPetal nextYellowLily;

    [Header("Purple Path")]
    public LilypadsPetal nextPurpleLily;

    [Header("Return Path")]
    public LilypadsPetal nextGreenLily;

    [Space]
    [Header("Refs")]
    public GameObject ClosedState;
    public GameObject OpenedState;
    public GameObject Gem;

    private LilyState currentState;

    // Use this for initialization
    void Start () {
		
	}

    public void OpenLily()
    {
        switch (StaffHeadColor.CurrentColorIndex)
        {
            case 0:
                SwitchToNone();
                break;
            case 1:
                SwitchToGreen();
                break;
            case 2:
                SwitchToPurple();
                break;
            case 3:
                SwitchToYellow();
                break;
            default:
                break;
        }
    }

    private void SwitchToYellow()
    {
        currentState = LilyState.Yellow;
        ClosedState.SetActive(false);
        OpenedState.SetActive(true);
    }

    private void SwitchToPurple()
    {
        currentState = LilyState.Purple;
        ClosedState.SetActive(false);
        OpenedState.SetActive(true);
    }

    /// <summary>
    /// This is the return path back to start from every spot
    /// </summary>
    private void SwitchToGreen()
    {
        currentState = LilyState.Green;
        ClosedState.SetActive(false);
        OpenedState.SetActive(true);
    }

    private void SwitchToNone()
    {
        currentState = LilyState.Closed;
        ClosedState.SetActive(true);
        OpenedState.SetActive(false);
    }

    public bool HasMoreJumps
    {
        get
        {
            switch (currentState)
            {
                case LilyState.Closed:
                    return false;
                case LilyState.Green:
                    return nextGreenLily != null;
                case LilyState.Purple:
                    return nextPurpleLily != null;
                case LilyState.Yellow:
                    return nextYellowLily != null;
                default:
                    return false;
            }
        }
    }
}
