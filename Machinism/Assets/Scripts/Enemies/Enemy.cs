using UnityEngine;
using EZCameraShake;


[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
	public GameObject coin;
	public GameObject deathParticles;
	[SerializeField] private int health = 10;

	protected int maxCoinAmount = 1;
	protected int minCoinAmount = 1;
	protected float circleUnitSize = 1;

	private bool hasHit;


	public void TakeDamage(int amount)
	{
		if ((health - amount) <= 0) OnDeath();
		health -= amount;

		var spriteFlash = GetComponentInChildren<SpriteFlashEffect>();
		if (spriteFlash != null) spriteFlash.Flash();
	}

	public void Kill()
	{
		health = 0;
		OnDeath();
	}

	protected virtual void OnDeath()
	{
		if (hasHit) return;
		hasHit = true;

		if (deathParticles != null) Instantiate(deathParticles, transform.position, Quaternion.identity);

		for (int i = 0; i < UnityEngine.Random.Range(minCoinAmount, maxCoinAmount + 1); i++)
		{
			Instantiate(coin,
						transform.position + (Vector3)Random.insideUnitCircle * circleUnitSize,
						Quaternion.identity);
		}

		CameraShaker.Instance.ShakeOnce(2.2f, 8, 0, 1);

		AudioManager.instance.Play("EnemyDeath");

		Destroy(gameObject);
	}
}
