using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphSwitch : MonoBehaviour {
    public GraphNode Target;
    public GraphNode NodeA;
    public GraphNode NodeB;

    public bool flip;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(flip)
        {
            Flip();
            flip = false;
        }
	}

    private void Flip()
    {
        for (int i = 0; i < Target.Nodes.Length; i++)
        {
            if (Target.Nodes[i] == NodeA)
            {
                Target.Nodes[i] = NodeB;
                return;
            }
            if(Target.Nodes[i] == NodeB)
            {
                Target.Nodes[i] = NodeA;
                return;
            }
        }
    }
}
