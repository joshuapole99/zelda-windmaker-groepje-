using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitPlayer : MonoBehaviour {

    private Vector3 _position;
    private Quaternion _rotation;
	// Use this for initialization
	void Start () {
        _position = transform.localPosition;
        _rotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = _position;
        transform.localRotation = _rotation;
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("test");
        }
    }
}
