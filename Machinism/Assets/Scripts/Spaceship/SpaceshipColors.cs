using System.Collections;
using UnityEngine;

public class SpaceshipColors : ReceiveCustomizedColors
{
	[SerializeField] protected float flashDuration = .15f;
	protected bool isFlashActive;
	public Color flashColor = Color.white;

	bool areInvincibilityFramesActive;


	private void Start() => AssignColorsFromTarget("SpaceshipColors");

	private void Update()
	{
		if (!areInvincibilityFramesActive && !isFlashActive)
		{
			SetSpriteColors(desiredColor);
			SetParticleColors(desiredColor);
		}

		// if invincibility frames are currently active and their time runs out, cut them off
		if (Timers.IsUp("SpaceshipInvFrames") && areInvincibilityFramesActive)
			areInvincibilityFramesActive = false;
	}

	public void OnInvincibilityFramesStart()
	{
		areInvincibilityFramesActive = true;

		var col = sprites[0].color; // get their color
		col.a = .3f;
		SetSpriteColors(col);
	}

	public void Flash() => StartCoroutine(nameof(FlashRoutine));

	protected IEnumerator FlashRoutine()
	{
		isFlashActive = true;
		SetSpriteColors(flashColor);
		yield return new WaitForSeconds(flashDuration);
		isFlashActive = false;
	}
}
