using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitPoint : MonoBehaviour {

    /// <summary>
    /// This script will manage the final hit point inside of the mouth of the boss
    /// Hitting this point will also lower it's health
    /// </summary>

    private BossDown[] _down;
    private GameObject[] _segments;
    private BoxCollider _col;
    private Light _light;
    public int health = 3;

	// Use this for initialization
	void Start ()
    {
        _segments = GameObject.FindGameObjectsWithTag("Segment");
        _down = new BossDown[_segments.Length];
        for (int i = 0; i < _segments.Length; i++)
        {
            _down[i] = _segments[i].GetComponent<BossDown>();
        }
        
        _light = gameObject.GetComponent<Light>();
	}	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < _segments.Length; i++)
        {
            if (_down[i].deactivate)
            {
                if (_col == null)
                {
                    _col = gameObject.AddComponent<BoxCollider>();
                    Debug.Log("Ik heb nu een collider!");
                }
                _light.enabled = true;
            }
            else
            {
                if (_col != null)
                {
                    Destroy(gameObject.GetComponent<BoxCollider>());
                }
                _light.enabled = false;
            }
            Debug.Log("in update: " + _down[i].deactivate);
        }
		
	}

    public void GetHit()
    {
        if (health > 0)
        {
            health--;
            Debug.Log("Health: " + health);
            for (int i = 0; i < _segments.Length; i++)
            {
                _down[i].StartCoroutine(_down[i].BossReanimate());
            }
        }
        if (health < 1)
        {
            BossDestroy _destroy = gameObject.GetComponentInParent<BossDestroy>();
            Debug.Log("Starting Coroutine");
            _destroy.StartCoroutine(_destroy.Detatch());
            Debug.Log("Started Coroutine");
        }
    }
}
