using UnityEngine;

public class TripleShotEnemyProjectile : Projectile
{
	Spaceship spaceship;

	protected override void Start()
	{
		base.Start();

		spaceship = FindObjectOfType<Spaceship>();

		sprite.transform.LookAtPos(spaceship.transform.position);
		MoveProjectile();
	}

	protected override void OnHitTarget(Collider2D target)
	{
		base.OnHitTarget(target);

		AudioManager.instance.Play("EnemyProjectileHit");
		SpaceshipHealth.TakeDamage(damage);
	}
}
