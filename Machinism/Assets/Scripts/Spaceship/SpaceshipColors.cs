using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipColors : MonoBehaviour
{
	public List<SpriteRenderer> sprites;

	[SerializeField] private float flashDuration = .15f;

	public Color desiredColor = Color.green;
	public Color flashColor = Color.white;

	bool areInvincibilityFramesActive;
	bool isFlashActive;



	private void Update()
	{
		if (!areInvincibilityFramesActive && !isFlashActive) SetSpriteColors(desiredColor);

		// if invincibility frames are currently active and their time runs out, cut them off
		if (Timers.IsUp("SpaceshipInvFrames") && areInvincibilityFramesActive)
			areInvincibilityFramesActive = false;
	}

	public void Flash() => StartCoroutine(nameof(FlashRoutine));

	private IEnumerator FlashRoutine()
	{
		isFlashActive = true;
		SetSpriteColors(flashColor);
		yield return new WaitForSeconds(flashDuration);
		isFlashActive = false;
	}

	public void OnInvincibilityFramesStart()
	{
		areInvincibilityFramesActive = true;

		var col = sprites[0].color; // get their color
		col.a = .3f;
		SetSpriteColors(col);
	}

	private void SetSpriteColors(Color color)
	{
		foreach (var sprite in sprites)
		{
			sprite.color = new Color(color.r, color.g, color.b, sprite.color.a);
		}
	}
}
