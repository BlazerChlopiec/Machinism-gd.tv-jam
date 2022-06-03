using UnityEngine;

public class FpsCounter : UIText
{
	protected override void Start()
	{
		base.Start();

		Application.targetFrameRate = 60;
	}

	private void Update() => field.text = (1f / Time.unscaledDeltaTime).ToString();
}
