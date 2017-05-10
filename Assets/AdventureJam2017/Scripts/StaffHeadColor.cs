using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffHeadColor : MonoBehaviour {

    /////// Static Methods
    const int GlobalVarID = 0;

    static StaffHeadColor me;
    public static Color CurrentColor
    {
        get { return me.StaffColors[me.currentColor]; }
    }


    /////// Public Methods
    public Color[] StaffColors;
    private int currentColor;

    [SerializeField]
    private SpriteRenderer StaffHead;

    public void Start()
    {
        me = this;
    }

    public void Switch()
    {
        currentColor = AC.GlobalVariables.GetIntegerValue(GlobalVarID);

        currentColor = ++currentColor% StaffColors.Length;
        StaffHead.color = StaffColors[currentColor];

        AC.GlobalVariables.SetIntegerValue(GlobalVarID, currentColor);
    }

    
    
}
