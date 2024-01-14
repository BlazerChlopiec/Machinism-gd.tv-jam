using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceiveCustomizedColors : MonoBehaviour
{
	public string externalColorIdentifier = "external";
	public bool assignExternalOnStartOnly = true;

	public Color desiredColor = Color.green;

	public List<SpriteRenderer> sprites;
	public List<ParticleSystem> particles;
	public List<Image> images;


	protected virtual void Start()
	{
		if (assignExternalOnStartOnly) AssignColorsFromTarget();
	}

	protected virtual void Update()
	{
		if (!assignExternalOnStartOnly) AssignColorsFromTarget();

		SetSpriteColors(desiredColor);
		SetParticleColors(desiredColor);
		SetImageColors(desiredColor);
	}

	protected void AssignColorsFromTarget()
	{
		var colorManager = FindObjectOfType<CustomizedColorsManager>();
		if (colorManager == null)
		{
			Debug.LogWarning("CustomizedColorsManager is non existent in the scene! Loading Defaults.");
			return;
		}

		var color = colorManager.GetColorFromIdentifier(externalColorIdentifier);
		desiredColor = color;
	}

	protected void SetSpriteColors(Color color)
	{
		foreach (var sprite in sprites)
		{
			sprite.color = new Color(color.r, color.g, color.b, sprite.color.a);
		}
	}
	protected void SetSpriteAlpha(float alpha, string[] excluded)
	{
		foreach (var sprite in sprites)
		{
			bool matches = false;
			foreach (var excludedNames in excluded)
			{
				matches = sprite.name == excludedNames;
				if (matches) break;
			}
			if (matches) continue;
			sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
		}
	}

	protected void SetSpriteAlpha(float alpha)
	{
		foreach (var sprite in sprites)
		{
			sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
		}
	}

	protected void SetParticleColors(Color color)
	{
		foreach (var particle in particles)
		{
			var main = particle.main;
			main.startColor = color;
		}
	}
	protected void SetImageColors(Color color)
	{
		foreach (var image in images)
		{
			image.color = new Color(color.r, color.g, color.b, image.color.a);
		}
	}
}
