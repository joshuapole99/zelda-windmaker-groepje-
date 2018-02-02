using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Playerjump : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private float JumpStrength = 300f;
    private float JumpLeft = 5f;
    private float JumpRight = 5f;
    private float JumpStop = 0f;
    private float gravity = 30f;
    private bool jumping = false;

    public Rigidbody _rigidbody;
    private Vector3 _inputDirection;



    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {

        // retrieve input
       // float x = Input.GetAxis("Horizontal");
       // float z = Input.GetAxis("Vertical");

       // _inputDirection = new Vector3(x, 0f, z);
        

    }











     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumping = true;
        }
        else{
            jumping = false;
        }
    }


    void FixedUpdate()
    {
        // move the player using physics
        _rigidbody.AddForce(_inputDirection * 300 * Time.fixedDeltaTime);

        if (Input.GetKeyDown("space") && Physics.Raycast(transform.position, Vector3.down, 1))
        {
            _rigidbody.AddForce(Vector3.up * JumpStrength);
        }

        /*if (Input.GetKeyDown(KeyCode.LeftArrow) && Physics.Raycast(transform.position, Vector3.down, 1))
        {
                    _rigidbody.AddForce(new Vector3(-60, 35, 0) * JumpLeft);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) && Physics.Raycast(transform.position, Vector3.down, 1))
        {
            _rigidbody.AddForce(Vector3.down * JumpStop);
        }


        if (Input.GetKeyDown(KeyCode.RightArrow) && Physics.Raycast(transform.position, Vector3.down, 1))
        {   
                _rigidbody.AddForce(new Vector3(60, 35, 0) * JumpRight);
        }


        if (Input.GetKeyUp(KeyCode.RightArrow) && Physics.Raycast(transform.position, Vector3.down, 1))
        {
            _rigidbody.AddForce(Vector3.down * JumpStop);
        }*/
        if (jumping = true)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && Physics.Raycast(transform.position, Vector3.down, 1))
            {
                _rigidbody.AddForce(new Vector3(-60, 35, 0) * JumpLeft);
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow) && Physics.Raycast(transform.position, Vector3.down, 1))
            {
                _rigidbody.AddForce(Vector3.down * JumpStop);
            }


            if (Input.GetKeyDown(KeyCode.RightArrow) && Physics.Raycast(transform.position, Vector3.down, 1))
            {
                _rigidbody.AddForce(new Vector3(60, 35, 0) * JumpRight);
            }


            if (Input.GetKeyUp(KeyCode.RightArrow) && Physics.Raycast(transform.position, Vector3.down, 1))
            {
                _rigidbody.AddForce(Vector3.down * JumpStop);
            }
        }

    }
}
