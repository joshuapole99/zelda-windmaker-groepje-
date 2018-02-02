using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIgnoreColission : MonoBehaviour
{

    [SerializeField]
    private Collider _leftHand;
    [SerializeField]
    private Collider _rightHand;
    private Collider _col;

    // Use this for initialization
    void Start()
    {
        _col = gameObject.GetComponent<Collider>();
        Physics.IgnoreCollision(_col, _leftHand);
        Physics.IgnoreCollision(_col, _rightHand);
    }
}
