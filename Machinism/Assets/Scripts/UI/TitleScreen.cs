using UnityEngine;

public class TitleScreen : MonoBehaviour
{
	[SerializeField] private KeyCode proceed = KeyCode.LeftShift;

	bool hasProceeded;

	private void Update()
	{
		if (Input.GetKeyDown(proceed) && !hasProceeded)
		{
			hasProceeded = true;
			TransitionManager.instance.Add("Space");
			AudioManager.instance.Play("UIClick");
		}
	}
}
