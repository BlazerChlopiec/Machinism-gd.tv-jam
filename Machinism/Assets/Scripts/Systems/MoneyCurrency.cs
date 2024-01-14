using UnityEngine;

public class MoneyCurrency : MonoSingleton<MoneyCurrency>
{
	public int value { get; protected set; }

	public void Add(int amount) => value += amount;
	public void Remove(int amount) => value -= amount;
}
