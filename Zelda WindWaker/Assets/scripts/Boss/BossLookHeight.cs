using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLookHeight : MonoBehaviour {
    /// <summary>
    /// A small script that moves an empty object along with the boss's height.
    /// It will make the boss face straight forward when it falls down.
    /// </summary>

    [SerializeField]
    private GameObject _boss;
    private GameObject _player;

    // Use this for initialization
	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(_player.transform.position.x, _boss.transform.position.y, _player.transform.position.z);
	}
}
