using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackFlip : MonoBehaviour {
    public float LeftZ;
    public float RightZ;

    public bool startLeft;

    private Transform myTransform;

    bool isLeft;

    private void Start()
    {
        isLeft = startLeft;
        myTransform = this.transform;
        SetRotation();
    }

    public void Switch()
    {
        isLeft = !isLeft; //flip
        SetRotation();
    }

    private void SetRotation()
    {
        myTransform.rotation = Quaternion.Euler(0, 0, isLeft ? LeftZ : RightZ);
    }
}
