using System.Collections;
using UnityEngine;

public class SpaceshipColors : ReceiveCustomizedColors
{
	[SerializeField] protected float customFlashDuration = .15f;
	protected bool isFlashActive;
	public Color customFlashColor = Color.white;

	bool areInvincibilityFramesActive;



	protected override void Update()
	{
		if (!areInvincibilityFramesActive && !isFlashActive)
		{
			base.Update();
		}

		// if invincibility frames are currently active and their time runs out, cut them off
		if (Timers.IsUp("SpaceshipInvFrames") && areInvincibilityFramesActive)
		{
			OnInvinciblityFramesEnd();
		}
	}

	private void OnInvinciblityFramesEnd()
	{
		areInvincibilityFramesActive = false;
		SetSpriteAlpha(1, excluded: new string[] { "Glow", "Laser" });
	}

	public void OnInvincibilityFramesStart()
	{
		areInvincibilityFramesActive = true;
		SetSpriteAlpha(.3f, excluded: new string[] { "Glow", "Laser" });
	}

	public void Flash() => StartCoroutine(nameof(FlashRoutine));

	protected IEnumerator FlashRoutine()
	{
		isFlashActive = true;
		SetSpriteColors(customFlashColor);
		yield return new WaitForSeconds(customFlashDuration);
		isFlashActive = false;
	}
}
