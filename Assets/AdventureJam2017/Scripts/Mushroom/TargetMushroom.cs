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

    private Transform imageTrans;

    public void Awake()
    {
        imageTrans = Image.gameObject.transform;
        Reset();
    }

    public void AddColor(Color c)
    {
        SetMinimums(c);

        var color = Image.color - c;
        color.r = Mathf.Max(color.r, minR);
        color.g = Mathf.Max(color.g, minG);
        color.b = Mathf.Max(color.b, minB);
        color.a = color.a + 0.5f / 6f;
        Image.color = color;
        imageTrans.localScale +=Vector3.one * 0.8f / 6;
        
        if (!CheckMatch() && (color == Color.white || color.a > 0.999f))//close enough, floating point rounding errors.
            LeanTween.delayedCall(1,Reset);
    }

    public void Reset()
    {
        Image.color = new Color(1, 1, 1, 0.5f);
        minR = 0;
        minG = 0;
        minB = 0;
        imageTrans.localScale = Vector3.one * 0.2f;
        particles.Stop();
    }

    public bool CheckMatch()
    {
        if(Image.color == TargetColor)
        {
            Debug.Log("WOOHOO");
            var parts = particles.main;
            parts.startColor = TargetColor;
            particles.Play();
            return true;
        }
        return false;
        
    }

    private void SetMinimums(Color c)
    {
        c = new Color32(85, 85, 85, 255) - c;
        minR = Mathf.Min(c.r + minR, 1f);
        minG = Mathf.Min(c.g + minG, 1f);
        minB = Mathf.Min(c.b + minB, 1f);
    }


}
