using UnityEngine;

public class Bounce : MonoBehaviour
{
	float yStartPos;
	float bounceSpeed;


	private void Start()
	{
		bounceSpeed = UnityEngine.Random.Range(.3f, 2.5f);
		yStartPos = transform.position.y;
	}

	private void Update()
	{
		transform.position = new Vector2(transform.position.x, yStartPos + Mathf.Sin(Time.time * bounceSpeed) / 2);
	}
}
