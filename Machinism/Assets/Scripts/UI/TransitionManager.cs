using UnityEngine;

public class TransitionManager : MonoSingleton<TransitionManager>
{
	public GameObject transitionPreset;


	public Transition Add(string sceneName)
	{
		var transition = Instantiate(transitionPreset, transform.position, Quaternion.identity).GetComponent<Transition>();
		transition.sceneName = sceneName;
		return transition;
	}
}
