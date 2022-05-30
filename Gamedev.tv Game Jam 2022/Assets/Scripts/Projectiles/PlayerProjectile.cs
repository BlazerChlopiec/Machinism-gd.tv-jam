using UnityEngine;

public class PlayerProjectile : Projectile
{
	protected override void Start()
	{
		base.Start();

		sprite.transform.LookAtMouse();
	}

	protected override void OnHitTarget(Collider2D target)
	{
		base.OnHitTarget(target);
		target.GetComponent<Enemy>().TakeDamage(1);
		AudioManager.instance.Play("PlayerProjectileHit");
	}
}
