using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunInteractionOnStaffColorChange : MonoBehaviour {

    public AC.Interaction ToRun;

	// Use this for initialization
	void OnEnable ()
    {
        StaffHeadColor.OnColorChanged += RunInteraction;
    }

    
	void OnDisable()
    {
        StaffHeadColor.OnColorChanged -= RunInteraction;
    }

    void RunInteraction()
    {
        ToRun.Interact();
    }
}
