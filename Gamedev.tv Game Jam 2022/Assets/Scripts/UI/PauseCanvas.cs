using System;
using UnityEngine;

public class PauseCanvas : MonoBehaviour
{
	public GameObject pauseContainer;

	public Transform inGameTimer;


	private void Start()
	{
		pauseContainer.SetActive(false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (!pauseContainer.activeInHierarchy) Pause();
			else Resume();
		}
	}

	private void Resume()
	{
		Time.timeScale = prevTimescale;
		pauseContainer.SetActive(false);

		if (inGameTimer != null) inGameTimer.SetParent(GameObject.Find("GameCanvas").transform);
	}

	float prevTimescale;
	private void Pause()
	{
		prevTimescale = Time.timeScale;
		Time.timeScale = 0;
		pauseContainer.SetActive(true);

		if (inGameTimer != null) inGameTimer.SetParent(GameObject.Find("PersistentCanvas").transform);
	}
}
