using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class StopVelocity : MonoBehaviour {

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = transform.GetComponent<Rigidbody2D>();
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector2.zero;
    }

}
