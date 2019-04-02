using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Handler : MonoBehaviour
{
    public int amountOfDashes = 4;
	private int hp = 1000;
    public Vector3 bomberPlace;
    public float bulletRange;
    private int dashCounter;
    private bool isDashing;
    private bool isInPosition;
    private Rigidbody2D _rb2d;
    private Vector2 moveVector;
    private float _timeLastShoted;
    
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Sprite _shootSprite;

    void Start()
    {
        bulletRange = -3f;                              // starting from 3
        _timeLastShoted = Time.time;
        isInPosition = false;
        dashCounter = 0;
        moveVector = new Vector2(-20, 0);
        isDashing = false;
        _rb2d = GetComponent<Rigidbody2D>();
        Invoke("BeginDash", 2);
    }


    private void BeginDash()
    {
        if (GetComponent<Rigidbody2D>().gravityScale != 0)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        isDashing = true;
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
            isDashing = false;
            _rb2d.AddForce(moveVector * (-12));         // -12 is knockback power when gitting the wall
            Invoke("BeginDash", 2);
            moveVector = -moveVector;
            dashCounter++;
        }
    }


    private void tpToBombingPosition()
    {
        transform.position = bomberPlace;
        _rb2d.velocity = new Vector2(0, 0);
        isInPosition = true;
    }


    void Update()
    {
        if (dashCounter == 4 && !isInPosition)
        {
            isDashing = false;
            Invoke("tpToBombingPosition", 3);
        }
        if (isDashing && !isInPosition)
        {
            _rb2d.AddForce(moveVector);
        }
        if (isInPosition)
        {
            projectileBombing();
        }
		if (hp <= 0)
			gameObject.SetActive(false);
	}

    // function for shooting
    private void projectileBombing()
    {
        
        if (1 / (Time.time - _timeLastShoted) < 1.3)          //bullet speed spawn; bigger ammount less speed 
        {
            GetComponent<SpriteRenderer>().sprite = _shootSprite;
            GameObject newBullet = Instantiate(_bullet, transform);
            newBullet.transform.localPosition = new Vector3(0, 0, 0);
            newBullet.transform.parent = null;
            _timeLastShoted = Time.time; 
            bulletRange += 0.2f;
            if (bulletRange >= 3)
            {
                GetComponent<Rigidbody2D>().gravityScale = 200;
                isInPosition = false;
                Invoke("BeginDash", 2);
                dashCounter = 0;
                bulletRange = -3;
            }
        }
    }
}
