using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_health : MonoBehaviour {
    public float health = 24;
    private float damage = 1;
    private AudioSource source;
    private bool hit = false;

    void Awake()
    {

        source = GetComponent<AudioSource>();

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Fireball")
        {
            hit = true;
            health -= damage;
            Debug.Log(health);
        }
        if (other.collider.tag == "hand")
        {
            hit = true;
            health -= damage * 2;
            Debug.Log(health);
        }
        if (health <= 0)
        {
            SceneManager.LoadScene(2);
        }
        if(hit == true)
        {
            GameObject[] _objects = GameObject.FindGameObjectsWithTag("Fireball");
            for (int i = 0; i < _objects.Length; i++)
            {
                Physics.IgnoreCollision(_objects[i].GetComponent<Collider>(), gameObject.GetComponent<Collider>());
            }
        }
    }
}
