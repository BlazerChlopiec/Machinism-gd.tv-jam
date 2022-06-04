using RandomGeneration;
using UnityEngine;

public class WaveCounter : UIText
{
	int currentWaveCount = 0;


	protected override void Start()
	{
		base.Start();

		AddOneToWave();
		FindObjectOfType<RandomGenerator>().OnNewWave += AddOneToWave;
	}

	private void AddOneToWave()
	{
		currentWaveCount++;
		field.text = "Wave " + currentWaveCount;

		// animation
		LeanTween.scale(gameObject, Vector3.one, 1f)
			.setEasePunch()
			.setIgnoreTimeScale(true);

		LeanTween.textColor(gameObject.GetComponent<RectTransform>(), Color.red, 1.3f)
			.setEasePunch()
			.setIgnoreTimeScale(true);
	}
}
