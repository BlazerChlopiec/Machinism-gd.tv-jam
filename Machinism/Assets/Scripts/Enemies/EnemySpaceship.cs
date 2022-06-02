using System.Collections;
using UnityEngine;

public class EnemySpaceship : Enemy
{
	protected Spaceship spaceship;

	public GameObject projectile;
	public GameObject shootParticles;
	public Transform shootOrigin;
	public Transform rotatedElements;

	[SerializeField] protected float shootFrequency = 2;
	[SerializeField] private float projectileSpeed = 60;
	[SerializeField] private int projectileDamage = 1;


	protected virtual void Start()
	{
		spaceship = FindObjectOfType<Spaceship>();
		StartCoroutine(Shoot());
	}

	protected virtual void Update()
	{
		rotatedElements.LookAtPosSmoothly(target: spaceship.transform.position);

		MoveToPlayer();
	}

	protected void MoveToPlayer()
	{
		if (!spaceship.isDead)
			transform.position = Vector2.Lerp(transform.position, spaceship.transform.position, .3f * Time.deltaTime);
	}

	public virtual IEnumerator Shoot()
	{
		yield return new WaitForSeconds(shootFrequency);
		StartCoroutine(Shoot());
	}

	protected void CreateProjectile()
	{
		if (transform.IsSeenByCamera() && !spaceship.isDead)
		{
			var proj = Instantiate(projectile, shootOrigin.position, Quaternion.identity).GetComponent<Projectile>();
			proj.speed = projectileSpeed;
			proj.damage = projectileDamage;

			//shoot particle
			Instantiate(shootParticles, shootOrigin.position, rotatedElements.rotation);

			AudioManager.instance.Play("EnemyShoot");
		}
	}
}
