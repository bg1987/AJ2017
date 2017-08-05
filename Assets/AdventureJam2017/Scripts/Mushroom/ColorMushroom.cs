using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMushroom : MonoBehaviour {

    public Color mushroomColor;
    public TargetMushroom target;
    public void OnClick()
    {
        target.AddColor(mushroomColor);
    }
}
