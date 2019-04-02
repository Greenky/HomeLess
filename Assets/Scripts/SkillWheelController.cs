using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWheelController : MonoBehaviour
{
	SpriteRenderer _rend;
	[SerializeField] private Sprite[] activeWheels;
	private MovementPlayer player;
    // Start is called before the first frame update
    void Start()
    {
		_rend = GetComponent<SpriteRenderer>();
		player = GameObject.Find("Player").GetComponent<MovementPlayer>();
		Debug.Log(player.activeSkillNumber);
		if (!player)
			Debug.Log("PLAYERA NEMA");
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			player.SetSkill(1);
			_rend.sprite = activeWheels[0];
			_rend.flipX = false;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			player.SetSkill(2);
			_rend.sprite = activeWheels[1];
			_rend.flipX = false;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			player.SetSkill(3);
			_rend.sprite = activeWheels[2];
			_rend.flipX = false;
		}
	}
}
