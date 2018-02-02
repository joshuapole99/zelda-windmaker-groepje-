using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDown : MonoBehaviour {

    private BossEye[] _eyes;
    private BossHover _hover;
    private BossMovement _movement;
    private int _activePoints;
    private Rigidbody _rb;
    public bool deactivate = false;
    [SerializeField]
    private GameObject _lookPoint;
    [SerializeField]
    private BossAttack _attack;
    [SerializeField]
    public int activeTimerMax = 1200;
    public int activeTimer;
    [SerializeField]
    private GameObject _teethLo;
    [SerializeField]
    private GameObject _teethHi;

    // Use this for initialization
    void Start ()
    {
        GameObject[] _eyeObjects = GameObject.FindGameObjectsWithTag("Eye");
        _eyes = new BossEye[_eyeObjects.Length];
        for (int i = 0; i < _eyeObjects.Length; i++)
        {
            _eyes[i] = _eyeObjects[i].GetComponent<BossEye>();
        }
        _hover = gameObject.GetComponent<BossHover>();
        _movement = gameObject.GetComponentInParent<BossMovement>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < _eyes.Length; i++)
        {
            if (_eyes[i].active)
            {
                _activePoints++;
            }
        }
        if (_activePoints == _eyes.Length && !deactivate)
        {
            _attack.StopAllCoroutines();
            if (_attack.hand != null)
            {
                _attack.StartCoroutine(_attack.EndSlap());
            }
            StartCoroutine(BossGoDown()); 
        }
        else
        {
            _activePoints = 0;
        }
        if (Input.GetKeyDown(KeyCode.O) && deactivate)
        {
            BossReanimate();
        }
        if (Input.GetKeyDown(KeyCode.P) && !deactivate)
        {
            for (int i = 0; i < _eyes.Length; i++)
            {
                _eyes[i].active = true;
            }
        }
        if (deactivate)
        {
            if (_lookPoint != null)
            {
                transform.LookAt(_lookPoint.transform);
            }
            activeTimer++;
        }
        else
        {
            activeTimer = 0;
            if (_teethLo != null && _teethLo.transform.localPosition.y < 0 && _movement.idle)
            {
                _teethLo.transform.localPosition += new Vector3(0f, 0.0002f, 0f);
                _teethHi.transform.localPosition -= new Vector3(0f, 0.0002f, 0f);
            }
        }
        if (activeTimer >= activeTimerMax)
        {
            _activePoints = 0;
            _rb.constraints = RigidbodyConstraints.None;
            Destroy(gameObject.GetComponent<Rigidbody>());
            _hover.enabled = true;
            _movement.enabled = true;
            _attack.enabled = true;
            _movement.idle = true;
            for (int i = 0; i < _eyes.Length; i++)
            {
                _eyes[i].active = false;
            }
            deactivate = false;
        }
	}

    IEnumerator BossGoDown()
    {
        _movement.enabled = false;
        deactivate = true;
        yield return new WaitForSeconds(1f);       
        _attack.enabled = false;
        Debug.Log("attack disabled");
        if (_rb == null)
        {
            _rb = gameObject.AddComponent<Rigidbody>();
        }
        if (_rb != null)
        {
            _rb.useGravity = true;
        }
        _hover.enabled = false;
        if (_teethLo != null)
        {
            while (_teethLo.transform.localPosition.y > -0.0018)
            {
                _teethLo.transform.localPosition -= new Vector3(0f, 0.0002f, 0f);
                _teethHi.transform.localPosition += new Vector3(0f, 0.0002f, 0f);
                yield return new WaitForEndOfFrame();
            }
        }
        yield return new WaitForSeconds(3);
        if (_rb != null)
        {
            _rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        yield return null;
    }
    public IEnumerator BossReanimate()
    {
        yield return new WaitForSeconds(1);
        _activePoints = 0;
        if (_rb != null)
        {
            _rb.constraints = RigidbodyConstraints.None;
        }
        Destroy(gameObject.GetComponent<Rigidbody>());
        _hover.enabled = true;
        _movement.enabled = true;
        _attack.enabled = true;
        _movement.idle = true;
        for (int i = 0; i < _eyes.Length; i++)
        {
            _eyes[i].active = false;
        }
        deactivate = false;
    }
}
