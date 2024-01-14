using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class WobbleListener : ButtonListener
{
	[SerializeField] private float wobbleSizeMultiplier = 1.5f;


	Vector2 prevScale;
	protected override void NewListener()
	{
		if (prevScale != Vector2.zero)
		{
			transform.localScale = prevScale;
			LeanTween.cancel(gameObject);
		}
		prevScale = transform.localScale;

		LeanTween.scale(gameObject, transform.localScale * wobbleSizeMultiplier, 1f)
			.setEasePunch()
			.setIgnoreTimeScale(true);
	}
}
