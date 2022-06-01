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
		OnChangeScene += ResetValuesOnSceneSwitch;
		OnChangeScene();
		SceneManager.LoadScene(sceneName);
	}

	private void ResetValuesOnSceneSwitch()
	{
		Time.timeScale = 1;
		SpaceshipHealth.maxHealth = 3;
		SpaceshipHealth.OnDeath = null;
		SpaceshipHealth.OnRefil = null;
		SpaceshipMoney.Remove(SpaceshipMoney.value);
	}
}
