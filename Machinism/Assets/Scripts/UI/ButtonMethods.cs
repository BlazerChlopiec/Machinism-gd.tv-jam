using UnityEngine;
using UnityEngine.UI;

public class ButtonMethods : MonoBehaviour
{
	public void PlayClickSound() => AudioManager.instance.Play("UIClick");

	public void ResetCurrentSceneWithTransition() => TransitionManager.instance.Reset();

	public void LoadSceneWithName(string sceneName) => TransitionManager.instance.Add(sceneName);
}
