using UnityEngine;

public class Coin : Affectable
{
	[Range(2, 20)] [SerializeField] private float speed = 5;
	float increasingSpeed;

	public GameObject collectParticles;

	Spaceship spaceship;


	private void Start()
	{
		spaceship = FindObjectOfType<Spaceship>();

		if (collectParticles == null) Debug.LogWarning("There is no collectParticles in Coin!");
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

		GetComponent<Bounce>().enabled = false;
	}

	public override void OnTouch()
	{
		SpaceshipMoney.Add(10);

		if (collectParticles) Instantiate(collectParticles, transform.position, Quaternion.identity);

		AudioManager.instance.Play("CoinCollect");

		Destroy(gameObject);
	}
}
