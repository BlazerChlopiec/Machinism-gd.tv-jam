using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DontDestroyOnLoad))]
public class Transition : MonoBehaviour
{
	[HideInInspector] public string sceneName;

	public Action OnChangeScene;


	public void ChangeScene()
	{
		if (OnChangeScene != null) OnChangeScene();
		Time.timeScale = 1;
		SceneManager.LoadScene(sceneName);
	}
}
