using UnityEngine;

public class SniperEnemyProjectile : Projectile
{
	Spaceship spaceship;

	protected override void Start()
	{
		base.Start();

		spaceship = FindObjectOfType<Spaceship>();

		sprite.transform.LookAtObject(spaceship.transform);
		MoveProjectile();
	}

	protected override void OnHitTarget(Collider2D target)
	{
		base.OnHitTarget(target);

		AudioManager.instance.Play("EnemyProjectileHit");
		SpaceshipHealth.TakeDamage(damage);
	}
}
