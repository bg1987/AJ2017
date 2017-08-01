using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackingTrack : MonoBehaviour {

    public Color[] CrackLevels;//should be sprites actually.
    public SpriteRenderer MySprite;
    public Rigidbody2D Rigid;

    private int index;
    private bool broken = false; //has the track broke/collapsed?

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (broken || collision.collider.gameObject.name != "Rock")
            return;

        index = Mathf.Min(index + 1, CrackLevels.Length - 1);

        broken = index == CrackLevels.Length - 1;

        SetColor();

        if(broken)
        {
            Rigid.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    // Use this for initialization
    void Start () {
        Rigid.bodyType = RigidbodyType2D.Static;
        index = 0;
        SetColor();
	}
	
    void SetColor()
    {
        MySprite.color = CrackLevels[index];
    }
}
