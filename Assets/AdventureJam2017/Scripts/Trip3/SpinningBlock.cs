using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBlock : MonoBehaviour {
    const int CHUNKS = 12;
    static bool[] Positions = new bool[CHUNKS];


    public int[] StonePositions;
    public int StaffColor;

    public bool Rotating;

    private int offset;
    private float initialZ;
    private int initialOffset;


    //These are for testing.
    public bool start;
    public bool rotate;
    public bool end;
	
    // Use this for initialization
	void Start ()
    {
        SetCurrentPosition();
        StaffHeadColor.OnColorChanged += StaffHeadColor_OnColorChanged;
    }

    private void StaffHeadColor_OnColorChanged()
    {
        if (StaffHeadColor.CurrentColorIndex == StaffColor)
        {
            StartRotate();
        }
        else
        {
            EndRotate();
        }
    }

    private void SetCurrentPosition(bool state = true)
    {
        foreach (int pos in StonePositions)
        {
            Positions[(pos + offset) % CHUNKS] = state;
        }
    }

    public void StartRotate()
    {
        Rotating = true;
        SetCurrentPosition(false);
        initialZ = gameObject.transform.transform.rotation.eulerAngles.z;
        initialOffset = offset;
    }

    public void Rotate()
    {
        if (!Rotating)
            return;
        gameObject.transform.Rotate(Vector3.forward * 360f / CHUNKS * -1);
        offset = (offset + 1) % CHUNKS;
    }

    public void EndRotate()
    {
        if (!Rotating)
            return;

        if(!IsCurrentPositionValid())
        {
            ReturnToInitialPosition();
        }
        SetCurrentPosition();
        Rotating = false;
    }

    private void ReturnToInitialPosition()
    {
        offset = initialOffset;
        gameObject.transform.eulerAngles = Vector3.forward * initialZ;
    }

    private bool IsCurrentPositionValid()
    {
        bool isValidSpot = true;
        foreach (int pos in StonePositions)
        {
            isValidSpot &= !Positions[(pos + offset) % CHUNKS];
        }

        return isValidSpot;
    }

    // Update is called once per frame
    void Update () {
        if(start)
        {
            start = false;
            StartRotate();
        }
		if (rotate)
        {
            rotate = false;
            Rotate();
        }
        if(end)
        {
            end = false;
            start = false;
            EndRotate();
        }
	}
}
