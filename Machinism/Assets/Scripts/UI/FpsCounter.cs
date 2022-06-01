using UnityEngine;

public class FpsCounter : UIText
{
	protected override void Start()
	{
		base.Start();

		Application.targetFrameRate = 60;
	}

	protected override void ApplyValue()
	{
		base.ApplyValue();

		field.text = (1f / Time.unscaledDeltaTime).ToString();
	}
}
