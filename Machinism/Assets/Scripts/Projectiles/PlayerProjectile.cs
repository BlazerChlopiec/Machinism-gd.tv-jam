using UnityEngine;

public class PlayerProjectile : Projectile
{
	protected override void Start()
	{
		base.Start();

		if (SystemInfo.deviceType == DeviceType.Desktop) sprite.transform.LookAtMouse();
		if (SystemInfo.deviceType == DeviceType.Handheld)
		{
			var spaceship = FindObjectOfType<Spaceship>();
			sprite.transform.rotation = spaceship.rotatedElements.rotation;
		}
	}

	protected override void OnHitTarget(Collider2D target)
	{
		base.OnHitTarget(target);
		target.GetComponent<Enemy>().TakeDamage(1);
		AudioManager.instance.Play("PlayerProjectileHit");
	}
}
