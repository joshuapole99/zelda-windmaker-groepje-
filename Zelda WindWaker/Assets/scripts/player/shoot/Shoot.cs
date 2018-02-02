using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
   
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            var mousePosition = new Vector3(ray.origin.x, transform.position.y, ray.origin.z);

            transform.LookAt(mousePosition + transform.position);
        

    }
}
                                  