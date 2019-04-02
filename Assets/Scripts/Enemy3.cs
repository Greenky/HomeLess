using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
	private int hp = 60;
    public float seeDistance = 20f;
    public float liftingHeigth;
    public float speed;
    public Animator animator;
    private Vector3 groundPosition;
    private Rigidbody2D _rb2d;
    private Vector3 playerPosition;
    private SpriteRenderer spriteRenderer;
    private bool isDashReady;
    private bool isDashing;
	public GameObject poof;

	protected void moveVertical()
    {
        transform.Translate(new Vector3(0, 8, 0) * Time.deltaTime);
    }


    void Start()
    {
        isDashReady = false;
        isDashing = false;
        groundPosition = transform.position;
        _rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Floor") || (collision.gameObject.tag == "Player"))
        {
            animator.SetBool("Attack", false);
            _rb2d.velocity = new Vector2(0, 0);
            groundPosition = transform.position;
            isDashing = false;
            isDashReady = false;
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Bullet")
		{
			hp--;
			collision.gameObject.SetActive(false);
		}
	}

	private void Dash()
    {
        if (isDashReady)
        {
            animator.SetBool("Attack", true);
            if (playerPosition.x - transform.position.x > 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            _rb2d.velocity = new Vector2(Mathf.Sign(playerPosition.x - transform.position.x) * speed, -speed);
            isDashReady = false;
        }
        isDashing = true;
        
    }


    void Update()
    {
		if (hp <= 0)
		{
			GameObject newPoof = Instantiate(poof, transform);
			newPoof.transform.localPosition = new Vector3(0, 0, -5);
			newPoof.transform.parent = null;
			gameObject.SetActive(false);

		}
		// Lifting
		if (!isDashing && !isDashReady)
        {
            moveVertical();
        }

        // Dashing 
        if (transform.position.y > (groundPosition.y + liftingHeigth))
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            if (!isDashReady)
            {
                isDashReady = true;
                //_rb2d.velocity = Vector2.zero;
            }
            //Debug.Log(Vector2.Distance(transform.position, playerPosition));
            if (Vector2.Distance(transform.position, playerPosition) < seeDistance)
            {
                Debug.Log("Dashing");
                Dash();
            }
        }
    }
}


// Near Attack
// transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);

