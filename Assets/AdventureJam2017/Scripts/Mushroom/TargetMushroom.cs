using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMushroom : MonoBehaviour {

    public SpriteRenderer Image;
    public Color TargetColor;
    public ParticleSystem particles;


    private float minR;
    private float minG;
    private float minB;

    public void Awake()
    {
        particles.Stop();
    }

    public void AddColor(Color c)
    {
        SetMinimums(c);

        var color = Image.color - c;
        color.r = Mathf.Max(color.r, minR);
        color.g = Mathf.Max(color.g, minG);
        color.b = Mathf.Max(color.b, minB);

        Image.color = color;
        if (color == Color.white)
            Reset();

        CheckMatch();
    }

    public void Reset()
    {
        Image.color = new Color(1, 1, 1, 1);
        minR = 0;
        minG = 0;
        minB = 0;
        particles.Stop();
    }

    public void CheckMatch()
    {
        if(Image.color == TargetColor)
        {
            Debug.Log("WOOHOO");
            var parts = particles.main;
            parts.startColor = TargetColor;
            particles.Play();
        }
        
    }

    private void SetMinimums(Color c)
    {
        c = new Color32(85, 85, 85, 255) - c;
        minR = Mathf.Min(c.r + minR, 1f);
        minG = Mathf.Min(c.g + minG, 1f);
        minB = Mathf.Min(c.b + minB, 1f);
    }


}
