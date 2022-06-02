using UnityEngine;

public class RocketEnemyProjectile : Projectile
{
	Spaceship spaceship;


	[SerializeField] private float timeToDestroy = 2.5f;

	protected override void Start()
	{
		base.Start();

		spaceship = FindObjectOfType<Spaceship>();

		sprite.transform.LookAtPos(spaceship.transform.position);

		Invoke(nameof(DestroyProjectile), timeToDestroy);
	}

	protected override void Update()
	{
		sprite.transform.LookAtPosSmoothly(spaceship.transform.position, smoothT: 6);

		base.Update();
	}

	protected override void OnHitTarget(Collider2D target)
	{
		base.OnHitTarget(target);

		SpaceshipHealth.TakeDamage(damage);
	}

	protected override void DestroyProjectile()
	{
		base.DestroyProjectile();

		AudioManager.instance.Play("RocketProjectileHit");
	}
}
