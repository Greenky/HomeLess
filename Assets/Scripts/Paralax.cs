using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
	[SerializeField] private Transform _camera;
	[SerializeField] private float _depth;
	Vector3 _oldCameraPos;
	// Start is called before the first frame update
	void Start()
    {
		_oldCameraPos = _camera.position;
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 delta;

		delta = _camera.position - _oldCameraPos;
		transform.position += new Vector3(delta.x * _depth, 0, 0);
		_oldCameraPos = _camera.position;
	}
}
