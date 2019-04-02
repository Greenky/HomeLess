using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {

    public float speed;
    private float _birthTime;
    public float lifeTime = 1;

    private void Start()
    {
        _birthTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate () {
		if (_birthTime + (lifeTime * 2) < Time.time)
			Destroy(gameObject);
		else if (_birthTime + lifeTime < Time.time)
			gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
		if (speed > 0)
			GetComponent<SpriteRenderer>().flipX = false;
		else
			GetComponent<SpriteRenderer>().flipX = true;

		transform.position += new Vector3(speed * 150 * Time.deltaTime, 0, 0);
		transform.localScale += new Vector3(0, speed * 30 * Time.deltaTime, 0);
		GetComponent<CapsuleCollider2D>().size += new Vector2(0, speed * 20 * Time.deltaTime);
	}

}
