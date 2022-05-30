using UnityEngine;

public class ButtonActions : MonoBehaviour
{
	public void PlayClickSound()
	{
		AudioManager.instance.Play("UIClick");
	}

	public void ResetSpaceScene()
	{
		var transition = TransitionManager.instance.Add("Space");
		transition.OnChangeScene += ResetValues;
	}

	private void ResetValues()
	{
		SpaceshipHealth.OnDeath = null;
		SpaceshipHealth.OnRefil = null;
		Money.Remove(Money.value);
	}
}
