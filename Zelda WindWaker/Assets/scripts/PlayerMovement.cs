using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public bool jump = false;
    private bool grounded = false;
    private bool ground;
    public float jumpForce = 1000f;

    public float speed;
    
    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 400.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
        
        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }
    void FixedUpdate ()
    {
        if (jump && ground)
        {

            GetComponent<Rigidbody>().AddForce(new Vector3(0f, jumpForce));
            jump = false;
            ground = false;
        }

    }
     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = true;
        }
    }

}
