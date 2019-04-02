using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoom : MonoBehaviour
{
	bool _startFade = false;
	bool _startUnFade = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.name == "Player")
		{
			//Debug.Log("Start");
			//_startUnFade = true;
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).gameObject.SetActive(false);
			}
		}
		 
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.name == "Player")
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).gameObject.SetActive(true);
			}
		}

	}
	/*
	private void Update()
	{
		if (_startFade)
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 10);
			}
		}
		if (_startUnFade)
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 10);
			}
			if (transform.GetChild(0).GetComponent<SpriteRenderer>().color.a == 0)
				_startUnFade = false;
		}
	}*/
}
