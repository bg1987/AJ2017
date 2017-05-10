using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffInteractionGlow : MonoBehaviour {

    [SerializeField]
    private Animator pingAnimator;

    [SerializeField]
    private DetectHotspots hotspotDetector;

    private bool pinging;

    private void Start()
    {

    }

    private void Update()
    {
        if (!pinging && hotspotDetector.GetAllDetectedHotspots().Length > 1)
        {
            pingAnimator.SetTrigger("ping");
        }

        //the staff hotspot is always there.
        pinging = hotspotDetector.GetAllDetectedHotspots().Length > 1;
    }

    
}
