using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBlock : MonoBehaviour {
    const int CHUNKS = 12;
    static bool[] OccupiedStonePositions = new bool[CHUNKS];


    public int[] StonePositions;
    public int StaffColor;

    public bool Rotating;

    private Color tintedColor;
    public SpriteRenderer tint;
    public SpriteRenderer mainImage;

    public int GlobalSolvedVarId;
    public int TargetOffset;
    public int LocalVarCorrectId;
    public AC.Interaction CheckWinActionList;
    
    private int offset;
    private float initialZ;
    private int initialOffset;

    // Use this for initialization
	void Start ()
    {
        SetCurrentPosition();
        StaffHeadColor.OnColorChanged += StaffHeadColor_OnColorChanged;
        tintedColor = tint.color;
        StaffHeadColor_OnColorChanged();
    }

    private void StaffHeadColor_OnColorChanged()
    {
        if (AC.GlobalVariables.GetBooleanValue(GlobalSolvedVarId))
            return;

        if (StaffColor != 0 && StaffHeadColor.CurrentColorIndex == StaffColor)
        {
            StartRotate();
            tint.color = Color.white;
            //LeanTween.color(tint.gameObject, new Color32(255,255,255,150), 0.5f);
            mainImage.sortingOrder = 2;
        }
        else
        {
            EndRotate();
        }
    }

    public void SetCurrentPosition()
    {
        SetCurrentPosition(true);
    }

    private void SetCurrentPosition(bool state = true)
    {
        foreach (int pos in StonePositions)
        {
            OccupiedStonePositions[(pos + offset) % CHUNKS] = state;
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
        gameObject.transform.Rotate((Vector3.forward * 360f / CHUNKS * -1));
        offset = (offset + 1) % CHUNKS;
        AC.GlobalVariables.SetBooleanValue(4, !IsCurrentPositionValid());
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
        //LeanTween.color(tint.gameObject, tintedColor, 0.5f);
        tint.color = tintedColor;
        mainImage.sortingOrder = 0;
        Rotating = false;
        AC.LocalVariables.SetBooleanValue(LocalVarCorrectId, offset == TargetOffset);

        CheckWinActionList.Interact();
    }

    public void Solve()
    {
        SetCurrentPosition(false);
        gameObject.transform.localRotation = Quaternion.Euler((Vector3.forward * 360f / CHUNKS * -1) * TargetOffset);
        offset = TargetOffset;
        AC.LocalVariables.SetBooleanValue(LocalVarCorrectId, offset == TargetOffset);
        SetCurrentPosition();
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
            isValidSpot &= !OccupiedStonePositions[(pos + offset) % CHUNKS];
        }

        return isValidSpot;
    }

    private void OnDestroy()
    {
        Debug.Log("leaving trip, resetting positions");
        OccupiedStonePositions = new bool[CHUNKS];
        StaffHeadColor.OnColorChanged -= StaffHeadColor_OnColorChanged; //TODO: This needs to be unassigned on solving the riddle.
    }
}
