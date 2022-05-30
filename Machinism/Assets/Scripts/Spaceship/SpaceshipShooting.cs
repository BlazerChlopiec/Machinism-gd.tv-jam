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
		if (Input.GetMouseButton(0) && Timers.IsUp("Shoot") && !spaceship.isDead)
		{
			Timers.New("Shoot", shootFrequency);
			Shoot();
		}
	}

	private void Shoot()
	{
		if (Time.timeScale == 0) return;

		Transform currentShootOrigin;
		MoveShootOriginIndex(out currentShootOrigin);

		var proj = Instantiate(projectile, currentShootOrigin.position, Quaternion.identity);
		proj.GetComponent<Projectile>().speed = projectileSpeed;
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
