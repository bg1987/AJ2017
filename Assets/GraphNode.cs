using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour {
    public static Dictionary<GraphNode, HashSet<GraphNode>> graph = new Dictionary<GraphNode, HashSet<GraphNode>>();

    public SpriteRenderer MySprite;
    public GraphNode[] Nodes;
    
    public Color color;

    public bool draw;
    public bool clear;

    public IEnumerable<GraphNode> Neighbors
    {
        get
        {
            return graph[this];
        }
    }

    public static bool showLines = true;
    public static bool clearColor = false;
	// Use this for initialization
	void Start () {
        if (!graph.ContainsKey(this))
            graph[this] = new HashSet<GraphNode>();
        //build the static graph
        foreach (var item in Nodes)
        {
            graph[this].Add(item);
            if (!graph.ContainsKey(item))
                graph[item] = new HashSet<GraphNode>();
            graph[item].Add(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (draw)
        {
            clearColor = false;

            draw = false;
            ColorConnectedNodes(this.color);
        }

        if(showLines)
        {
            foreach (var n in Neighbors)
            {
                Debug.DrawLine(transform.position, n.gameObject.transform.position);
            }
        }
        
        if(clear)
        {
            clear = false;
            clearColor = true;
        }

        if(clearColor)
        {
            MySprite.color = Color.white;
        }
	}

    public void ClearColors()
    {
        foreach(var n in graph.Keys)
        {
            n.MySprite.color = Color.white;
        }
    }

    void ColorConnectedNodes(Color c)
    {
        //this should protect from infinite loops
        if (MySprite.color == c)
            return;
        Debug.Log("Coloring " + gameObject.name);
        MySprite.color = c;
        foreach (var n in Neighbors)
        {
            n.ColorConnectedNodes(c);
        }
    }
}
