using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour {
    public static Dictionary<GraphNode, HashSet<GraphNode>> graph = new Dictionary<GraphNode, HashSet<GraphNode>>();

    public SpriteRenderer MySprite;
    public GraphNode[] Nodes;
    
    public Color color;

    public bool isTarget;
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
	// Use this for initialization
	void Start () {
        if (MySprite == null)
        {
            MySprite = this.GetComponent<SpriteRenderer>();
        }
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
            draw = false;
            ColorConnectedNodes(this.color);
        }

        if(showLines)
        {
            foreach (var n in Neighbors)
            {
                Debug.DrawLine(transform.position, n.gameObject.transform.position,Color.green);
            }
        }
        
        if(clear)
        {
            clear = false;
            ClearColors();
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
        MySprite.color = c;

        
        if (isTarget)
            return;

        foreach (var n in Neighbors)
        {
            n.ColorConnectedNodes(c);
        }
    }
}
