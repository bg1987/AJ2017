using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSwitch : MonoBehaviour {
    public float RotationStateA;
    public float RotationStateB;

    public bool flip;

    public GraphNode Red;
    public GraphNode Blue;
    public GraphNode Green;

    private GraphSwitch graphSwitch;
    private Transform myTransform;
	// Use this for initialization
	void Start () {
        RotationStateA = RotationStateA < 0 ? 360 + RotationStateA : RotationStateA;
        RotationStateB = RotationStateB < 0 ? 360 + RotationStateB : RotationStateB;

        myTransform = transform;
        myTransform.rotation = Quaternion.Euler(0, 0, RotationStateA);
        graphSwitch = GetComponent<GraphSwitch>();
        Green.draw = true;
        Blue.draw = true;
        Red.draw = true;
    }

    void Update()
    {
        if(flip)
        {
            Flip();
            flip = false;
        }
    }

    public void Flip()
    {
        
        if (Mathf.Abs(myTransform.rotation.eulerAngles.z - RotationStateA)<1f)
        {
            myTransform.rotation = Quaternion.Euler(0, 0, RotationStateB);
        }
        else if(Mathf.Abs(myTransform.rotation.eulerAngles.z - RotationStateB)<1f)
        {
            myTransform.rotation = Quaternion.Euler(0, 0, RotationStateA);
        }

        if (graphSwitch != null)
            graphSwitch.Flip();
        Green.ClearColors();
        Green.draw = true;
        Blue.draw = true;
        Red.draw = true;
    }
}
