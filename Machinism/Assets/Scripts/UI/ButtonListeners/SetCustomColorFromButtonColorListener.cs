using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SetCustomColorFromButtonColorListener : ButtonListener
{
	[SerializeField] private string targetIdentifier;

	protected override void NewListener()
	{
		var colorManager = FindObjectOfType<CustomizedColorsManager>();
		if (colorManager == null)
		{
			Debug.LogError("CustomizedColorsManager is non existent in the scene!");
			return;
		}

		colorManager.SetColorToTarget(targetIdentifier, GetComponent<Button>().colors.normalColor);
	}
}
