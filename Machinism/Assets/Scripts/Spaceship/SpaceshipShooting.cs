using System.Collections.Generic;
using UnityEngine;

public class SpaceshipShooting : MonoBehaviour
{
	public List<Transform> shootOrigin;

	Spaceship spaceship;

	public GameObject projectile;
	public GameObject shootParticle;

	public float shootFrequency = .1f;
	public float projectileSpeed = 30;
	public float projectileSize = 1;


	private void Start() => spaceship = GetComponent<Spaceship>();

	private void Update()
	{
		if (SystemInfo.deviceType == DeviceType.Desktop)
		{
			if (Input.GetMouseButton(0) && Timers.IsUp("Shoot") && !spaceship.isDead)
			{
				Timers.New("Shoot", shootFrequency);
				Shoot();
			}
		}

		else if (SystemInfo.deviceType == DeviceType.Handheld)
		{
			var mobileShootAccuracy = 5f;
			var maxDistance = 20f;

			// the position ensures that the cast starts in front of the ship (so you can't shoot backwards when someone gets close to the back)
			var hit = Physics2D.CircleCast(transform.position + spaceship.rotatedElements.transform.up * mobileShootAccuracy,
										   mobileShootAccuracy, spaceship.rotatedElements.transform.up,
										   maxDistance, LayerMask.GetMask("Enemy"));

			if (hit && Timers.IsUp("Shoot") && !spaceship.isDead) // sphere cast until hits enemy
			{
				Timers.New("Shoot", shootFrequency);
				Shoot(hit.point);
			}
		}
	}

	private void Shoot(Vector2 customProjectileDir = default(Vector2))
	{
		if (Time.timeScale == 0) return;

		Transform currentShootOrigin;
		MoveShootOriginIndex(out currentShootOrigin);

		var proj = Instantiate(projectile, currentShootOrigin.position, Quaternion.identity);
		proj.GetComponent<PlayerProjectile>().speed = projectileSpeed;
		if (customProjectileDir != Vector2.zero) proj.GetComponent<PlayerProjectile>().customDirection = customProjectileDir;
		proj.transform.localScale = Vector3.one * projectileSize;

		//shoot particles
		Instantiate(shootParticle, currentShootOrigin.position, currentShootOrigin.rotation);

		AudioManager.instance.Play("PlayerShoot");
	}


	int originIndex = 0;
	private void MoveShootOriginIndex(out Transform origin)
	{
		origin = shootOrigin[originIndex].transform;

		originIndex++;
		if (originIndex >= shootOrigin.Count)
			originIndex = 0;
	}
}
