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
	}
}
