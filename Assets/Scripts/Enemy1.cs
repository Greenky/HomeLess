using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
	private int hp = 20;
    private Rigidbody2D _rb2d;
    private CircleCollider2D myCircleCollider;
    private float lastSlimeSpawnTime;
    private Vector3 slimePos;

    public float myVelocity = 20f;
    public GameObject slime;
    public float moveDirection;
    public float myWeidth;
    public float myHeight;
	public GameObject poof;



	protected void CreateSlime(Vector3 slimePos)
    {
        Instantiate(slime, slimePos, Quaternion.identity);
    }

    protected void move()
    {
        _rb2d.velocity = new Vector2(moveDirection * myVelocity, _rb2d.velocity.y);
     
        
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
        if (collision.gameObject.tag == "Wall")
        {
            GetComponent<SpriteRenderer>().flipX = (!GetComponent<SpriteRenderer>().flipX ? true : false);
 
            moveDirection = (-1) * moveDirection;
        }
    }

    void Start()
    {
        myHeight = transform.localScale.y;
        moveDirection = 1;
        _rb2d = GetComponent<Rigidbody2D>();
        lastSlimeSpawnTime = Time.time;
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
		if (Time.time - lastSlimeSpawnTime > 1)
        {
            slimePos = new Vector3(transform.position.x, transform.position.y);
            CreateSlime(slimePos);
            lastSlimeSpawnTime = Time.time;
        }
        this.move();
    }

    
}


