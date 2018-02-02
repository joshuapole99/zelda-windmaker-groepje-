using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    /// <summary>
    /// This script will determine the movement of the boss.
    /// this includes flying around, facing the player,
    /// keeping distance from the player and staying close enough to
    /// the center of the arena so it won't flee to unreachable places.
    /// </summary>

    [SerializeField]
    private GameObject _target;
    public float targetDistance;
    [SerializeField]
    private GameObject _center;
    private float _centerDistance;
    [SerializeField]
    private int _nextMove;
    private int _moveTimer;
    public float moveSpeed; // tells the attack script if the boss is moving or not
    public bool idle; // tells the attack script whether the boss is in it's idle state or not
    public float rand;

	// Use this for initialization
	void Start ()
    {
        _moveTimer = 0;
        idle = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // the boss will always look at the player
        transform.LookAt(_target.transform);
        transform.localPosition = new Vector3(transform.localPosition.x, 8f, transform.localPosition.z);
        // movement will only be performed while the boss is idle
        if (idle)
        {
            // a looping timer for the DoNextMove function, to make the boss move around the player in random directions
            if (_moveTimer >= _nextMove)
            {
                rand = Random.Range(0f, 3f);
                _moveTimer = 0;
            }
            _moveTimer++;
            MoveAround();
            // movement from the DoNextMove is applied every frame
            transform.Translate(Vector3.left * moveSpeed);
            

            // every frame the boss checks it's distance between him and the player, as well as the distance between him and the center of the arena
            _centerDistance = Vector3.Distance(transform.position, _center.transform.position);
            targetDistance = Vector3.Distance(transform.position, _target.transform.position);

            // if the boss gets too close to the player, it needs to create some distance, but needs to mind the distance from the center
            if (targetDistance < 8 && _centerDistance < 25)
            {
                transform.Translate(-Vector3.forward * 0.1f);
            }

            // if the boss gets too far away from the player, he must come closer
            else if (targetDistance > 18)
            {
                transform.Translate(Vector3.forward * 0.1f);
            }

            // if the boss gets too far away from the center of the arena, he must move closer
            if (_centerDistance > 25)
            {
                transform.Translate(Vector3.forward * 0.1f);
            }
        }
    }

    void MoveAround()
    {
        /// <summary>
        /// this function makes the boss move left, right or stay in place, depending on a random number
        /// </summary>

        if (rand < 1 && moveSpeed > -0.25f)
        {
            moveSpeed -= 0.005f;
        }
        else if (rand < 2 && moveSpeed < 0.25f)
        {
            moveSpeed += 0.005f;
        }
        else
        {
            if (moveSpeed > 0)
            {
                moveSpeed -= 0.01f;
            }
            else if (moveSpeed < 0)
            {
                moveSpeed += 0.01f;
            }
        }
    }
}
