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
        myTransform = transform;
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
            if (graphSwitch != null)
                graphSwitch.Flip();

            Green.draw = true;
            Blue.draw = true;
            Red.draw = true;
            flip = false;
        }
    }

    public void Flip()
    {
        if (myTransform.rotation.eulerAngles.z == RotationStateA)
        {
            myTransform.rotation = Quaternion.Euler(0, 0, RotationStateB);
        }
        else if(myTransform.rotation.eulerAngles.z == RotationStateB)
        {
            myTransform.rotation = Quaternion.Euler(0, 0, RotationStateA);
        }
    }
}
