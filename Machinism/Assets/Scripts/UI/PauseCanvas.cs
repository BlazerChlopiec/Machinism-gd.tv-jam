using System;
using UnityEngine;

public class PauseCanvas : DynamicCanvas
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			Toggle();
		}
	}

	public override void Close()
	{
		base.Close();

		Time.timeScale = prevTimescale;
	}

	float prevTimescale;
	public override void Open()
	{
		base.Open();

		prevTimescale = Time.timeScale;
		Time.timeScale = 0;
	}
}
