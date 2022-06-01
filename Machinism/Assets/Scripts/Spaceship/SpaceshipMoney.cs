using UnityEngine;

public class SpaceshipMoney : MonoBehaviour
{
	MoneyUIText moneyUIText;

	public static int value { get; protected set; }


	private void Start() => moneyUIText = FindObjectOfType<MoneyUIText>();
	private void Update() => moneyUIText.field.text = value.ToString() + "$";
	public static void Add(int amount) => value += amount;
	public static void Remove(int amount) => value -= amount;
}
