using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMushroom : MonoBehaviour {

    public SpriteRenderer Image;
    public Color TargetColor;

    public void AddColor(Color c)
    {
        Image.color -= c;
    }

    public void Reset()
    {
        Image.color = new Color(1, 1, 1, 1);
    }

    public void CheckMatch()
    {
        if(Image.color == TargetColor)
        {
            Debug.Log("WOOHOO");
        }
    }
}
