using UnityEngine;

public class Collectable : Affectable
{
	[Range(2, 20)] [SerializeField] private float speed = 5;
	float increasingSpeed;

	public GameObject collectParticles;

	Spaceship spaceship;


	private void Start()
	{
		spaceship = FindObjectOfType<Spaceship>();

		if (collectParticles == null) Debug.LogWarning("There is no collectParticles in Collectable!");
	}

	private void Update()
	{
		if (affected && !spaceship.isDead)
		{
			increasingSpeed += Time.deltaTime * 3;
			transform.position = Vector2.Lerp(transform.position, spaceship.transform.position,
								(speed + increasingSpeed) * Time.deltaTime);
		}
	}

	public override void OnAffected()
	{
		base.OnAffected();

		var bounce = GetComponent<Bounce>();
		if (bounce != null) bounce.enabled = false;
	}

}
