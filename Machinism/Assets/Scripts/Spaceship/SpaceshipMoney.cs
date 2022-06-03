using UnityEngine;

public class SpaceshipMoney : MonoBehaviour
{
	public static int value { get; protected set; }


	public static void Add(int amount) => value += amount;
	public static void Remove(int amount) => value -= amount;
}
