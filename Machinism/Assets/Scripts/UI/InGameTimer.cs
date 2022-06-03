using UnityEngine;

public class InGameTimer : UIText
{
	private float timeToDisplay;

	private void Update()
	{
		timeToDisplay += Time.deltaTime;
		float minutes = Mathf.FloorToInt(timeToDisplay / 60);
		float seconds = Mathf.FloorToInt(timeToDisplay % 60);
		field.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}
}
