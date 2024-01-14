using UnityEngine;

public class GemCurrency : MonoSingleton<GemCurrency>
{
	public int value { get; protected set; }


	private void Start() => value = PlayerPrefs.GetInt("GemCurrency", 0);

	public void Add(int amount)
	{
		value += amount;

		PlayerPrefs.SetInt("GemCurrency", value);
	}
	public void Remove(int amount)
	{
		value -= amount;

		PlayerPrefs.SetInt("GemCurrency", value);
	}
}
