using UnityEngine;

public class Meteor : Enemy
{
	[SerializeField] private float fallSpeed = 1;

	protected float xMultiplier;
	protected float randomScale;
	protected float rotateSpeed;

	SpriteRenderer sprite;


	protected virtual void Start()
	{
		sprite = GetComponentInChildren<SpriteRenderer>();

		randomScale = UnityEngine.Random.Range(1f, 2f);
		transform.localScale = Vector2.one * randomScale;// size is random from 1 and 2

		xMultiplier = UnityEngine.Random.Range(-2, 2);
		rotateSpeed = UnityEngine.Random.Range(1, 3);
	}

	private void Update()
	{
		transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
		transform.Translate(Vector2.right * xMultiplier * Time.deltaTime);

		sprite.transform.Rotate(new Vector3(0, 0, rotateSpeed) * 30 * Time.deltaTime);
	}
}
