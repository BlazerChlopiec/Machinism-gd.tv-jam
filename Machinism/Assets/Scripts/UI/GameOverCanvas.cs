using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCanvas : DynamicCanvas
{
	public override void Open()
	{
		base.Open();

		Time.timeScale = 0;

		AudioManager.instance.Play("GameOver");
	}
}
