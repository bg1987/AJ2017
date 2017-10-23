using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphTarget : MonoBehaviour {

    public GraphNode ColorTarget;
    public int VariableID;

    SpriteRenderer mySprite;

    private void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
    }
    void Update () {
        AC.LocalVariables.SetBooleanValue(VariableID, ColorTarget.color == mySprite.color);
	}
}
