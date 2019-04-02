using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
	public bool _isSound;
	public bool _isEfects;
	private bool _battleTime;
	private GameObject _pauseMenu;

	private void Awake()
	{
		_isSound = true;
		_isSound = false;
		_battleTime = false;
		_pauseMenu = null;
	}

	public void OnStartClick()
	{
		SceneManager.LoadScene("Scene_v0.2");
	}

	public void OnComtinueClick()
	{
		Time.timeScale = 1;
		_pauseMenu.SetActive(false);
	}

	public void OnRestartClick()
	{
		SceneManager.LoadScene("Scene_v0.2");
	}

	public void OnExitClickPause()
	{
		SceneManager.LoadScene("MeinMenu");
	}

	public void OnExitClick()
	{
		Application.Quit();
	}

	// Update is called once per frame
	void Update()
    {
		if (SceneManager.GetActiveScene().name == "Scene_v0.2")
		{
			CinemachineVirtualCamera cinema = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
			float OrSize = cinema.m_Lens.OrthographicSize;
			if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Scene_v0.2")
			{
				Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
				if (Time.timeScale == 0)
					_pauseMenu.SetActive(true);
				else
					_pauseMenu.SetActive(false);

			}

			if (GameObject.Find("Player") && GameObject.Find("Player").transform.position.x > 1600 && OrSize < 160)
			{
				GameObject.Find("Boss1").GetComponent<Boss1Handler>().enabled = true;
				GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
				if (camera.GetComponent<AudioSource>().enabled == true)
				{
					camera.GetComponent<AudioSource>().enabled = false;
					cinema.gameObject.GetComponent<AudioSource>().Play();
					_battleTime = true;
				}
				cinema.m_Lens.OrthographicSize += 2 * Time.deltaTime;
			}

			if (!cinema.gameObject.GetComponent<AudioSource>().isPlaying && _battleTime && !GameObject.Find("Boss1").GetComponent<AudioSource>().isPlaying)
			{
				GameObject.Find("Boss1").GetComponent<AudioSource>().Play();
			}
		}
	}

	public void	Music()
	{
		_isSound = (_isSound) ? false : true;
	}

	public void FX()
	{
		_isEfects = (_isEfects) ? false : true;
	}

	private void OnLevelWasLoaded(int level)
	{
		if (GameObject.Find("Comt"))
			GameObject.Find("Comt").SetActive(true);
		GameObject player = GameObject.Find("Player");
		if (player)
		{
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().enabled = _isSound;
			foreach (AudioSource audio in player.GetComponents<AudioSource>())
			{
				audio.enabled = _isEfects;
			}
		}
		Time.timeScale = 1;
		if (!_pauseMenu && GameObject.Find("PauseMenu"))
		{
			_pauseMenu = GameObject.Find("PauseMenu").gameObject;
			_pauseMenu.SetActive(false);
		}
		else if (GameObject.Find("PauseMenu"))
			_pauseMenu.SetActive(false);
	}

	public void Death()
	{
		Time.timeScale = 0;
		_pauseMenu.SetActive(true);
		GameObject.Find("Comt").SetActive(false);
	}

}
