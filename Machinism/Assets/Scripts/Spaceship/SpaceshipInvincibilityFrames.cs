using UnityEngine;

public class SpaceshipInvincibilityFrames : MonoBehaviour
{
	public SpriteRenderer sprite;

	Color startColor;
	bool ActiveInvincibilityFrames;


	private void Start() => startColor = sprite.color;

	private void Update()
	{
		// if invincibility frames are currently active and their time runs out, cut them off
		if (Timers.IsUp("SpaceshipInvFrames") && ActiveInvincibilityFrames)
			OnInvincibilityFramesStop();
	}

	public void OnInvincibilityFramesStart()
	{
		ActiveInvincibilityFrames = true;

		var col = sprite.color;
		col.a = .3f;
		sprite.color = col;
	}

	public void OnInvincibilityFramesStop()
	{
		ActiveInvincibilityFrames = false;
		sprite.color = startColor;
	}
}
