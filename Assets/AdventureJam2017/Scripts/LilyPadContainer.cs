using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPadContainer : MonoBehaviour {

    public static LilyPadContainer instance;

    public LilypadsPetal startingLily;
    public LilypadsPetal currentlySelectedLily;

    public void Awake()
    {
        instance = this;
        //currentlySelectedLily = startingLily;
    }

    private void OnEnable()
    {
        StaffHeadColor.OnColorChanged += UpdateLilyState;
    }

    private void UpdateLilyState()
    {
        if(currentlySelectedLily == null)
        {
            LilypadsPetal.UpdateLily(startingLily);
        }
        else
        {
            currentlySelectedLily.UpdateAffectedLilies();
        }
    }

    private void OnDisable()
    {
        StaffHeadColor.OnColorChanged -= UpdateLilyState;
    }
}
