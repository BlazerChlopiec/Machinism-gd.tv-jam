using UnityEngine;

public class SpaceshipAffectors : MonoBehaviour
{
	CircleCollider2D circle { get; set; }

	public float radius = 2;


	private void Start()
	{
		circle = GetComponent<CircleCollider2D>();
	}

	private void Update()
	{
		circle.radius = radius;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Affectable"))
		{
			collision.GetComponent<Affectable>().OnAffected();
		}
	}
}
