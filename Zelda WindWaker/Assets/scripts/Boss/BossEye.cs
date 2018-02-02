using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEye : MonoBehaviour
{
    /// <summary>
    /// This small script handles the colours of the weak spots
    /// </summary>
   
    private Renderer _rend;
    private Light _light;
    public bool active; // this bool is public because it needs to be accessible to another script

    // Use this for initialization
    void Start ()
    {     
        _rend = gameObject.GetComponent<Renderer>();
        _light = gameObject.GetComponent<Light>();
        active = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (active)
        {
            _rend.material.color = Color.gray;
            _light.enabled = false;
        }
        else
        {
            _rend.material.color = Color.red;
            _light.enabled = true;
        }
	}

    public void GetHit()
    {
        active = true;
    }
}
