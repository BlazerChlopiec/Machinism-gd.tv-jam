using System.Collections.Generic;
using UnityEngine;

public class ReceiveCustomizedColors : MonoBehaviour
{
	public Color desiredColor = Color.green;

	public List<SpriteRenderer> sprites;
	public List<ParticleSystem> particles;


	protected void AssignColorsFromTarget(string identifier)
	{
		var colorManager = FindObjectOfType<CustomizedColorsManager>();
		if (colorManager == null)
		{
			Debug.LogError("CustomizedColorsManager is non existent in the scene!");
			return;
		}

		var color = colorManager.GetColorFromIdentifier(identifier);
		desiredColor = color;
	}

	protected void SetSpriteColors(Color color)
	{
		foreach (var sprite in sprites)
		{
			sprite.color = new Color(color.r, color.g, color.b, sprite.color.a);
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
}
