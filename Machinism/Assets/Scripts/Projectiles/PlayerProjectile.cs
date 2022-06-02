using UnityEngine;

public class PlayerProjectile : Projectile
{
	public Vector2 customDirection;

	protected override void Start()
	{
		base.Start();

		if (SystemInfo.deviceType == DeviceType.Desktop) sprite.transform.LookAtMouse();
		if (SystemInfo.deviceType == DeviceType.Handheld) sprite.transform.LookAtPos(customDirection);

	}

	protected override void OnHitTarget(Collider2D target)
	{
		base.OnHitTarget(target);
		target.GetComponent<Enemy>().TakeDamage(1);
		AudioManager.instance.Play("PlayerProjectileHit");
	}
}
