using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
	private int hp = 40;
    private Rigidbody2D _rb2d;
    private CircleCollider2D myCircleCollider;
    public float myVelocity = 20f;
    public GameObject poof;

	public float moveHorizontalDirection;
    public float moveVerticalDirection;

    public Transform sightStart;    
    public Transform sightEnd;


    protected void moveVertical()
    {
        _rb2d.velocity = new Vector2(_rb2d.velocity.x, moveVerticalDirection * myVelocity);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Bullet")
		{
			hp--;
			collision.gameObject.SetActive(false);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Floor") || (collision.gameObject.tag == "Enemy"))
        {
            moveVerticalDirection = (-1) * moveVerticalDirection;
        }
    }

    void Start()
    {
        moveHorizontalDirection = 1;
        moveVerticalDirection = 1;
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		if (hp <= 0)
		{
			GameObject newPoof = Instantiate(poof, transform);
			newPoof.transform.localPosition = new Vector3(0, 0, -5);
			newPoof.transform.parent = null;
			gameObject.SetActive(false);
			
		}
        this.moveVertical();
    }


}


