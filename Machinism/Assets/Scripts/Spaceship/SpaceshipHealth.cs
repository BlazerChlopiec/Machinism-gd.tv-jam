using System;
using UnityEngine;

public class SpaceshipHealth : MonoBehaviour
{
	HealthUIText healthUIText;
	static SpriteFlashEffect spriteFlash;

	private static int health;
	public static float maxHealth = 3; // has to be float to be upgraded in the shop

	public static Action OnDeath;
	public static Action OnRefil;


	private void Start()
	{
		healthUIText = FindObjectOfType<HealthUIText>();
		spriteFlash = GetComponentInChildren<SpriteFlashEffect>();
	}

	private void Update() => healthUIText.field.text = health.ToString();

	public static void TakeDamage(int amount)
	{
		if (Timers.IsUp("SpaceshipInvFrames"))
		{
			health -= amount;
			spriteFlash.Flash();

			if (health <= 0)
			{
				Kill();
			}
		}
	}

	public static void RefillHealth()
	{
		health = (int)maxHealth;
		if (OnRefil != null) OnRefil();
	}

	public static void Kill()
	{
		if (Timers.IsUp("SpaceshipInvFrames"))
		{
			health = 0;
			AudioManager.instance.Play("PlayerDeath");
			if (OnDeath != null) OnDeath();
			//AudioManager.instance.Play("Death");
		}
	}
}
