using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraPlayFunctions : MonoBehaviour {
    

    public void StopMovment()
    {
        var rig = GetComponent<Rigidbody2D>();
        rig.velocity = Vector2.zero;
        rig.angularDrag = 0;
        rig.angularVelocity = 0;
    }
}
