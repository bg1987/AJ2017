using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffHeadColor : MonoBehaviour {

    /////// Static Methods
    const int GlobalVarID = 0;

    public static event System.Action OnColorChanged;
    static StaffHeadColor me;
    public static Color CurrentColor
    {
        get { return me.StaffColors[me.currentColor]; }
    }

    public static int CurrentColorIndex
    {
        get { return me.currentColor; }
    }
    /////// Public Methods
    public Color[] StaffColors;
    private int currentColor;

    [SerializeField]
    private SpriteRenderer StaffHead;
    [SerializeField]
    private Animator StaffHeadAnimator;
    [SerializeField]
    private float OffDelay = 0.5f;

    private bool canClick = true;
    public void Start()
    {
        me = this;
        currentColor = AC.GlobalVariables.GetIntegerValue(GlobalVarID);
        StaffHead.color = CurrentColor;
        StaffHeadAnimator.SetInteger("ColorNumber", CurrentColorIndex);
    }

    public void Switch()
    {
        if (!canClick)
            return;

        currentColor = AC.GlobalVariables.GetIntegerValue(GlobalVarID);

        currentColor = ++currentColor% StaffColors.Length;


        

        StaffHeadAnimator.SetInteger("ColorNumber", currentColor);

        AC.GlobalVariables.SetIntegerValue(GlobalVarID, currentColor);
        
        if(currentColor == 0)
        {
            canClick = false;
            LeanTween.delayedCall(OffDelay, () => 
            {
                canClick = true;
                StaffHead.color = StaffColors[currentColor];
                if (OnColorChanged != null)
                    OnColorChanged();
            });
        }
        else
        {
            StaffHead.color = StaffColors[currentColor];
            if (OnColorChanged != null)
                OnColorChanged();
        }
    }

    
    
}
