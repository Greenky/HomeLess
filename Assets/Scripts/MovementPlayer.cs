using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovementPlayer : MonoBehaviour {

	public int activeSkillNumber = 0;
	// 1 - doublejump
	// 2 - power dash
	// 3 - block
	private float shootDirection = 1;
    private float velocityMaximum = 30;
    private float speed = 70f;
    private float _lives = 100f;
    private float _dashStartTime;
    private bool _dashStarted;
	private bool _inAir;
	private bool _inShell;
	private bool _doubleJump;
	private bool _wheelActivated;
    private Rigidbody2D _rb2d;
	[SerializeField] private Image _hpBar;
	[SerializeField] private GameObject _bullet;
    [SerializeField] private float _shootRate = 10;
    [SerializeField] private Sprite _shell;
    [SerializeField] private Sprite _standSprite;
    [SerializeField] private GameObject _skillWheel;
    [SerializeField] private GameObject _shield;
    [SerializeField] private GameObject[] _skillShells;
	[SerializeField] private Animator _animator;
	[SerializeField] private AudioSource _runSound;
	[SerializeField] private AudioSource _shootSound;
	[SerializeField] private AudioSource _jumpSound;
	[SerializeField] private AudioSource _hitTheGroungSound;
	[SerializeField] private AudioSource _dashSound;
	[SerializeField] private AudioSource _minusHPSound;
	private GameObject _wheel;
    private float _timeLastShoted;
	private GameObject _tempShield;


	void Start () {
		foreach (GameObject shell in _skillShells)
		{
			shell.SetActive(false);
		}
        _rb2d = GetComponent<Rigidbody2D>();
        _timeLastShoted = Time.time;
		_dashStarted = false;
		_inShell = false;
		_tempShield = null;
	}

    // Update is called once per frame
    void Update()
    {
		//Debug.Log("Tag - " + transform.tag);
		if (_inShell == false)
		{
			// Movement
			float moveHorizontal = Input.GetAxis("Horizontal");
			if (Input.GetKeyDown(KeyCode.RightArrow))
				shootDirection = 1;
			else if (Input.GetKeyDown(KeyCode.LeftArrow))
				shootDirection = -1;

			if (moveHorizontal != 0 && !_inAir)
			{
				_animator.SetBool("Running", true);
				_runSound.Play();
			}
			else
			{
				_runSound.Stop();
				_animator.SetBool("Running", false);
			}
			Vector2 movement = new Vector2(moveHorizontal, 0) * speed;
			//if (Mathf.Abs(_rb2d.velocity.x) < velocityMaximum)
			_rb2d.position += movement * Time.deltaTime;
			//_rb2d.AddForce(movement * speed);


			if (shootDirection == -1)
			{
				GetComponent<SpriteRenderer>().flipX = true;
				foreach (GameObject shell in _skillShells)
					shell.GetComponent<SpriteRenderer>().flipX = true;
			}
			if (shootDirection == 1)
			{
				GetComponent<SpriteRenderer>().flipX = false;
				foreach (GameObject shell in _skillShells)
					shell.GetComponent<SpriteRenderer>().flipX = false;
			}
			// Jumping
			if (Input.GetKeyDown(KeyCode.UpArrow) && !_inAir)
			{
				_jumpSound.Play();
				_animator.SetBool("InAir", true);
				_rb2d.AddForce(new Vector2(0, 60 / Time.fixedDeltaTime));
				if (activeSkillNumber == 1 && _doubleJump)
				{
					_doubleJump = false;

				}
				else
					_inAir = true;
			}

			// Shoting
			if (Input.GetKey(KeyCode.Space))
			{
				_animator.SetBool("Shooting", true);
				if (1 / (Time.time - _timeLastShoted) < _shootRate)
				{
					//if (!_shootSound.isPlaying)
						_shootSound.Play();
					GameObject newBullet = Instantiate(_bullet, transform);
					_bullet.GetComponent<BulletMove>().speed = shootDirection;
					newBullet.transform.localPosition = new Vector3(3f * shootDirection, 1.2f, -1);
					newBullet.transform.parent = null;
					_timeLastShoted = Time.time;
				}
			}
			else
			{
				_animator.SetBool("Shooting", false);
			}

			//Skills
			if (Input.GetKeyDown(KeyCode.Tab))
			{
				_wheelActivated = (_wheelActivated ? false : true);
				if (_wheelActivated)
				{
					Time.timeScale = 0.2f;
					Time.fixedDeltaTime = 0.001f;
					_wheel = Instantiate(_skillWheel, transform);
					_wheel.transform.localPosition = new Vector3(0, 8, -1);
				}
				else
				{
					if (_wheel)
						Destroy(_wheel);
					Time.fixedDeltaTime = 0.02f;
					Time.timeScale = 1f;
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			if (activeSkillNumber == 2)
			{
				_rb2d.AddForce(new Vector2(shootDirection * 200 / Time.fixedDeltaTime, 0));
				_dashStartTime = Time.time;
				_dashStarted = true;
				_dashSound.Play();
			}
			else if (activeSkillNumber == 3)
			{
				if (_inShell)
				{
					_animator.SetBool("Hiding", false);
					Invoke("UnSetSheald", 0.4f);
				}
				else
				{
					_inShell = true;
					_animator.SetBool("Hiding", true);
					_skillShells[2].SetActive(false);
					Invoke("SetSheald", 0.4f);
				}
			}
		}


		
			
	}


	public void	Restart()
	{
		SceneManager.LoadScene("Scene_v0.2");
	}

	void SetSheald()
	{
		//GetComponent<SpriteRenderer>().sprite = _shell;
		if (!_tempShield)
		{
			_tempShield = Instantiate(_shield, transform);
			_tempShield.transform.localPosition = new Vector3(0, 1, -1);
		}
		else
		{
			_tempShield.SetActive(true);
		}
	}

	void UnSetSheald()
	{
		_skillShells[2].SetActive(true);
		_inShell = false;
		//GetComponent<SpriteRenderer>().sprite = _shell;
		if (_tempShield)
			_tempShield.SetActive(false);
	}

	private void FixedUpdate()
	{
		// LIfes
		_hpBar.fillAmount = _lives / 100;
		if (_lives <= 0)
		{
			GameObject.Find("GameLogic").GetComponent<GameLogic>().Death();
			//SceneManager.LoadScene("Scene_v0.2");
		}

		if (_dashStartTime + 0.1f < Time.time && _dashStarted)
		{
			_rb2d.velocity = Vector2.zero;
			_dashStarted = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor" || collision.collider.tag == "Wall")
		{
			_hitTheGroungSound.Play();
			_inAir = false;
			_animator.SetBool("InAir", false);
			_doubleJump = true;
		}
		if (collision.collider.tag == "Enemy")
		{
			if (_inShell)
				_lives += 10;
			else
			{
				_minusHPSound.Play();
				_lives -= 10;
			}
			_rb2d.velocity = Vector2.zero;
			_rb2d.AddForce(collision.contacts[0].normal * 70 / Time.fixedDeltaTime);
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "DeathZone")
			_lives = 0;
		if (collision.tag == "Enemy")
		{
			_lives -= 20;
		}
	}

	public void SetSkill(int skillNumber)
	{
		activeSkillNumber = skillNumber;
		foreach (GameObject shell in _skillShells)
			shell.SetActive(false);
		_skillShells[skillNumber - 1].SetActive(true);
	}
}
