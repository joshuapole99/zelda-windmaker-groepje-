using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    /// <summary>
    /// This script takes care of the attacks of the boss
    /// </summary>

    private BossMovement _movement;
    [SerializeField]
    private GameObject _leftHand;
    [SerializeField]
    private GameObject _leftChild;
    [SerializeField]
    private GameObject _rightHand;
    [SerializeField]
    private GameObject _rightChild;
    [SerializeField]
    private int _attackTimer;
    [SerializeField]
    private int _attackTimerMax;
    private Vector3 _handStart;
    public int rand; // a random number to roll for an attack
    private int _slapTime;
    private float _handDistance;
    private Vector3 _position;
    [SerializeField]
    private GameObject _teethLo;
    [SerializeField]
    private GameObject _teethHi;  
    [SerializeField]
    private GameObject _ball;
    [SerializeField]
    private GameObject _ballOrigin;
    public GameObject hand;
    private GameObject _headParent;

    // Use this for initialization
    void Start ()
    {

        _movement = GetComponentInParent<BossMovement>();
        _attackTimer = 0;
        _slapTime = 0;
        _position = transform.localPosition;
    }
	
	// Update is called once per frame
	void Update ()
    {

        // a timer for attack that only build up when the boss isn't moving (hovering not counted as moving)
		if (_attackTimer >= _attackTimerMax && _movement.rand > 2)
        {
            _movement.idle = false;

            // the random number has a bigger range than the amount of attacks, thanks to this the boss won't have a set time between 2 attacks
            rand = Random.Range(0, 5);
            ChooseAttack();
            _attackTimer = 0;
        }
        if (_movement.idle)
        {
            _attackTimer++;

            // local position will be written to _position as long as the boss is idle
            _position = transform.localPosition;
        }

        // _position writes the local position back every frame, this makes sure the boss doesn't move away while attacking
        transform.localPosition = _position;

        // the bases of the hands will always face the boss's head
        _leftHand.transform.LookAt(gameObject.transform);
        _rightHand.transform.LookAt(gameObject.transform);

        // when the hands are too close to the head after an attack, they will move away
        if (Vector3.Distance(_leftHand.transform.position, transform.position) < 2)
        {
            _leftHand.transform.Translate(Vector3.forward * 0.2f);
        }
        if (Vector3.Distance(_rightHand.transform.position, transform.position) < 2)
        {
            _rightHand.transform.Translate(Vector3.forward * 0.2f);
        }

    }

    void ChooseAttack()
    {
        ///<summary>
        /// This function tells what attack to perform depending on a randomly generated number
        ///</summary>

        _slapTime = 0;
        switch (rand)
        {
            case 1:
                StartCoroutine(Slap(_leftHand, _leftChild, 1));
                Debug.Log("Slap Left!");
                break;
            case 2:   
                StartCoroutine(Slap(_rightHand, _rightChild, -1));
                Debug.Log("Slap Right!");
                break;
            case 3:
                StartCoroutine(Fireballs());
                Debug.Log("Shoot Fireballs");
                break;
            default:
                _movement.idle = true;
                break;
        }
    }

    IEnumerator Slap(GameObject _hand, GameObject child, int direction)
    {
        ///<summary>
        /// This coroutine is used for both slap attacks, it will first define what hand to move, then extend it's range, swing it's hand around and return it to it's starting position.
        ///</summary>

        // the starting position of the hand is being stored in _handStart
        _handStart = _hand.transform.localPosition;
        hand = _hand;
        // the distance between the hand and head will be stored
        _handDistance = Vector3.Distance(_hand.transform.position, gameObject.transform.position);
        
        // distance between head and hand is increased to the distance between the head and the player
        while (_handDistance < _movement.targetDistance - 2)
        {
            _hand.transform.Translate(Vector3.forward * -0.1f);
            _handDistance = Vector3.Distance(_hand.transform.position, gameObject.transform.position);
            yield return new WaitForEndOfFrame();
        }
        Rigidbody rb = child.AddComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
        }
        yield return new WaitForSeconds(1);       
        // the hand is swung around the head
        while (_slapTime < 30)
        {
            _hand.transform.Translate(Vector3.left * 1.3f * direction);
            if (_hand.transform.position.y > 3)
            {
                _hand.transform.Translate(Vector3.up * -2f);
            }
            _slapTime++;
            yield return new WaitForEndOfFrame();
        }
        Destroy(child.GetComponent<Rigidbody>());
        yield return new WaitForSeconds(1);
        Debug.Log("slap finished");
        StartCoroutine(EndSlap());
        yield return null;
    }
    public IEnumerator EndSlap()
    {
        // the hand is being moved back towards it's starting position
        if (_handStart != new Vector3(0f, 0f, 0f))
        {
            while (Vector3.Distance(hand.transform.localPosition, _handStart) > 0.2f)
            {
                hand.transform.localPosition = Vector3.Lerp(hand.transform.localPosition, _handStart, 0.1f);
                yield return new WaitForEndOfFrame();
            }
            hand.transform.localPosition = _handStart;
            yield return new WaitForSeconds(0.5f);
        }
        // the boss's state is set back to idle to end the attack
        _movement.idle = true;
        Debug.Log("end slap finished");
        yield return null;
    }

    IEnumerator Fireballs()
    {
        int i = 0;
        while (i < 9)
        {
            _teethLo.transform.localPosition -= new Vector3(0f, 0.0002f, 0f);
            _teethHi.transform.localPosition += new Vector3(0f, 0.0002f, 0f);
            i++;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        GameObject[] _balls = new GameObject[20];
        int j = 0;
        while (j < 20)
        {
            _balls[j] = _ball;
            Instantiate(_balls[j], _ballOrigin.transform);
            j++;
            yield return new WaitForSeconds(0.25f);
        }
        yield return new WaitForSeconds(0.5f);
        
        // the boss will won't end it's attack until all fireballs are destroyed.
        while (GameObject.FindGameObjectsWithTag("Fireball").Length != 0)
        {
            yield return new WaitForEndOfFrame();
        }
        _movement.idle = true;
        Debug.Log("fireball attack finished");
        yield return null;
    }
}