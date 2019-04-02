using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_follow : MonoBehaviour {

    public Transform player;
    private Vector3 _offset;
	// Use this for initialization
	void Start () {
        _offset = transform.position - player.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x + _offset.x, transform.position.y, transform.position.z), 1);
	}
}
