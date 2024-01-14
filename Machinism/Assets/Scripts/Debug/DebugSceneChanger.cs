using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyDebug
{
	public class DebugSceneChanger : MonoSingleton<DebugSceneChanger>
	{
		[SerializeField] private KeyCode resetSceneKey = KeyCode.R;

		[Space(10)]
		[SerializeField] private List<DebugSceneChange> sceneChanges = new List<DebugSceneChange>();

		private void Update()
		{
			if (Input.GetKeyDown(resetSceneKey))
			{
				SceneManager.LoadScene(Current);

				Time.timeScale = 1;
				SpaceshipHealth.OnDeath = null;
				SpaceshipHealth.OnRefil = null;
				MoneyCurrency.instance.Remove(MoneyCurrency.instance.value);
			}

			foreach (var changes in sceneChanges)
			{
				if (Input.GetKeyDown(changes.key) && Current != changes.sceneIndex)
				{
					SceneManager.LoadScene(changes.sceneIndex);
				}
			}
		}

		private int Current => SceneManager.GetActiveScene().buildIndex;
	}

	[System.Serializable]
	public class DebugSceneChange
	{
		public int sceneIndex;
		public KeyCode key;
	}
}
