using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffInteractionGlow : MonoBehaviour {

    [SerializeField]
    private float moveTime;
    [SerializeField]
    private float endZ;
    [SerializeField]
    private Light glowLight;
    [SerializeField]
    private DetectHotspots hotspotDetector;

    private Transform lightTransform;
    private LTDescr tween;
    private float startZ;

    private void Start()
    {
        lightTransform = glowLight.transform;
        startZ = lightTransform.position.z;
    }

    private void Update()
    {
        glowLight.color = StaffHeadColor.CurrentColor;

        //the staff hotspot is always there.
        if(hotspotDetector.GetAllDetectedHotspots().Length > 1)
        {
            if (tween == null)
            {
                tween = LeanTween.moveLocalZ(glowLight.gameObject, endZ, moveTime).setLoopPingPong().setEaseOutBack().setRepeat(-1);
            }
        }
        else
        {
            if (tween != null)
            {
                LeanTween.cancel(glowLight.gameObject);
                tween = LeanTween.moveLocalZ(glowLight.gameObject, startZ, moveTime / 2f);
                tween = null;
            }
        }

    }

    
}
