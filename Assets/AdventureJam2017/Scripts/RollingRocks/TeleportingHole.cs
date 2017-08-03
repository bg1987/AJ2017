using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingHole : MonoBehaviour {

    public Transform Location;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name != "Rock")
            return;

        Vector3 pos = Location == null ? Vector3.one * 1000 : Location.position;
        collider.gameObject.SendMessage("Stop");
        collider.gameObject.transform.position = pos; //BAD BENNY!
    }
}
