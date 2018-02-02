using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHover : MonoBehaviour
{
    /// <summary>
    /// This script adds a hovering effect to the boss,
    /// the effect is created by adding random movement within a certain range
    /// </summary>

    [SerializeField]
    private int _nextHover;
    private int _hoverTimer;
    private Vector3 _hoverVector; // doesn't have to be a Vector3, but is an easy way to store 3 randomly generated values that are used for a single functionality
    private Vector3 _position; // stores the boss's position so the boss won't wander off by hovering too far away
    [SerializeField]
    private bool _moveUp; // tells the movement randomizer to limit vertical movement depending on the boss's height
    private Quaternion _nullRotation;

    // Use this for initialization
    void Start ()
    {
        _hoverTimer = 0;
        _position = transform.localPosition;

        // for improved visual effect, the boss will move from te very start
        _hoverVector = new Vector3(Random.Range(-0.025f, 0.025f), Random.Range(-0.025f, 0.025f), Random.Range(-0.025f, 0.025f));
        _moveUp = true;
        _nullRotation = transform.localRotation;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // a looping timer for the Hover function, to make the boss feel more alive
        if (_hoverTimer >= _nextHover)
        {
            Hover();
            _hoverTimer = 0;
        }
        _hoverTimer++;

        // hovering directions generated are applied every frame
        transform.Translate(Vector3.forward * _hoverVector.x);
        transform.Translate(Vector3.left * _hoverVector.z);
        transform.Translate(Vector3.up * _hoverVector.y);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, _nullRotation, 10f);

        // if the boss flies too high, he must float down
        if (_position.y > 5)
        {
            transform.Translate(Vector3.up * -0.1f);
        }

        // if the boss gets too low to the ground, he must fly up
        else if (_position.y < -5)
        {
            transform.Translate(Vector3.up * 0.5f);
        }
        _position = transform.localPosition;

        // keep boss in place while still hovering around
        if (_position.x > 2)
        {
            transform.localPosition -= new Vector3(0.1f, 0f, 0f);
        }
        if (_position.x < -2)
        {
            transform.localPosition += new Vector3(0.1f, 0f, 0f);
        }
        if (_position.z > 2)
        {
            transform.localPosition -= new Vector3(0f, 0f, 0.1f);
        }
        if (_position.z < -2)
        {
            transform.localPosition += new Vector3(0f, 0f, 0.1f);
        }
    }

    void Hover()
    {
        /// <summary>
        /// this function will generate 3 random values between -0.025 and 0.025, these values are used to give the boss a hovering effect by slightly moving it around in all directions
        /// </summary>
        _hoverVector.x = Random.Range(-0.0125f, 0.0125f);
        if (gameObject.transform.localPosition.y > 3 && _moveUp)
        {
            _moveUp = false;
        }
        if (gameObject.transform.localPosition.y < -3 && !_moveUp)
        {
            _moveUp = true;
        }
        if (_moveUp)
        {
            _hoverVector.y = Random.Range(-0.001f, 0.0125f);
        }
        else
        {
            _hoverVector.y = Random.Range(-0.0125f, 0.001f);
        }

        _hoverVector.z = Random.Range(-0.0125f, 0.0125f);
    }
}
