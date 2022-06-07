using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomizedColorsManager : MonoSingleton<CustomizedColorsManager>
{
	public List<StartColorChange> startColorChanges;


	public Color GetColorFromIdentifier(string identifier)
	{
		Color color = Color.clear;

		foreach (var changes in startColorChanges)
		{
			if (changes.identifier == identifier)
			{
				color = changes.desiredColor;
			}
		}

		return color;
	}

	public void SetColorToTarget(string identifier, Color targetColor)
	{
		foreach (var changes in startColorChanges)
		{
			if (changes.identifier == identifier)
			{
				changes.desiredColor = targetColor;
			}
		}
	}
}

[Serializable]
public class StartColorChange
{
	public string identifier;
	public Color desiredColor = Color.white;
}
