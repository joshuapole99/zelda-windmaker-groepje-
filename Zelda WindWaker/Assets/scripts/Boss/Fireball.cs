using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    /// <summary>
    /// This script Controls the behaviour of the fireballs.
    /// </summary>

    private Transform _origin; // the center of the mouth of the boss
    private GameObject _player;
    private int _speed; // the speed which the fireball moves at

	// Use this for initialization
	void Start ()
    {
        // the fireballs will never exist longer than 5 seconds
        Destroy(gameObject, 5);

        _origin = transform;
        _player = GameObject.FindGameObjectWithTag("Player");
        _speed = 0;

        // 2 random numbers are created for both horizontal and vertical offset from the creation point
        float randomH = Random.Range(-0.2f, 0.2f);
        float randomV = Random.Range(-0.2f, 0.2f);
        transform.localPosition += new Vector3(randomH, randomV, 0f);

        // distance between fireball's randomized position and starting position is limited
        float distance = Vector3.Distance(transform.position, _origin.position);
        if ( distance > 0.2f)
        {
            transform.LookAt(_origin);
            transform.Translate(Vector3.forward * (distance - 0.2f));
        }
        // first the fireball creates the impression of being forged from nothing by growing from barely visible to it's full size
        StartCoroutine(Appear());

	}

    void Update()
    {
        // applies speed to position of the fireball
        transform.Translate(Vector3.forward * _speed);
    }

    void Shoot()

    {
        _speed = 1;
    }

    private void OnCollisionEnter(Collision other)
    {
        // the fireball will be destroyed as soon as it hits the player or floor
        if (other.collider.tag == "Player" || other.collider.tag == "Floor")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Appear()
    {
        /*
         * before the fireball starts to grow it will decide the player's location to move towards,
         * this gives the player time to move away while the fireball is still growing and enables evasion.
        */
        transform.LookAt(_player.transform);
        for (int i = 0; i < 90; i++)
        {
            transform.localScale += new Vector3(0.0015f, 0.0015f, 0.0015f);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.5f);

        // after the fireball has grown to it's full size, it will be shot towards the player
        Shoot();
        yield return null;
    }

}
