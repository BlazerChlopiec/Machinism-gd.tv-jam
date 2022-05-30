using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCanvas : MonoBehaviour
{
	public GameObject gameOverContainer;

	public Transform inGameTimer;


	private void Start() => gameOverContainer.SetActive(false);

	public void Show()
	{
		Time.timeScale = 0;

		inGameTimer.SetParent(GameObject.Find("PersistentCanvas").transform);
		gameOverContainer.SetActive(true);

		AudioManager.instance.Play("GameOver");
	}
}
