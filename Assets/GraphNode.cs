using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour {

    public SpriteRenderer MySprite;
    public GraphNode[] Nodes;
    
    public Color color;

    public bool draw;
    public bool clear;


    public static bool showLines = true;
    public static bool clearColor = false;
	// Use this for initialization
	void Start () {
		
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
            foreach (var n in Nodes)
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


    void ColorConnectedNodes(Color c)
    {
        Debug.Log("Coloring " + gameObject.name);
        MySprite.color = c;
        foreach (var n in Nodes)
        {
            n.ColorConnectedNodes(c);
        }
    }
}
