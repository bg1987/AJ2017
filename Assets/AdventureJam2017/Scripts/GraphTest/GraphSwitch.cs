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

    public void Flip()
    {

        if (GraphNode.graph[Target].Contains(NodeA))
        {
            GraphNode.graph[Target].Remove(NodeA);
            GraphNode.graph[NodeA].Remove(Target);

            GraphNode.graph[Target].Add(NodeB);
            GraphNode.graph[NodeB].Add(Target);
        }
        else if (GraphNode.graph[Target].Contains(NodeB))
        {
            GraphNode.graph[Target].Remove(NodeB);
            GraphNode.graph[NodeB].Remove(Target);

            GraphNode.graph[Target].Add(NodeA);
            GraphNode.graph[NodeA].Add(Target);
        }

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
