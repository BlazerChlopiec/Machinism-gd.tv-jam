using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoSingleton<TransitionManager>
{
	public GameObject transitionPreset;


	/// <summary>
	/// Adds a transition that directs to the scane called sceneName
	/// </summary>
	public Transition Add(string sceneName)
	{
		var transition = Instantiate(transitionPreset, transform.position, Quaternion.identity).GetComponent<Transition>();
		transition.sceneName = sceneName;
		return transition;
	}

	/// <summary>
	/// Adds a transition that resets the current scene
	/// </summary>
	public Transition Reset()
	{
		var transition = Instantiate(transitionPreset, transform.position, Quaternion.identity).GetComponent<Transition>();
		transition.sceneName = SceneManager.GetActiveScene().name;
		return transition;
	}
}
