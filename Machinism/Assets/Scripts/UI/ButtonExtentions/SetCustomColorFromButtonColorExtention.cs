using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SetCustomColorFromButtonColorExtention : MonoBehaviour
{
	[SerializeField] private string targetIdentifier;


	public void SetColorToTarget()
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
