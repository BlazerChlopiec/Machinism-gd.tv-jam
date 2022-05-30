using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
	[HideInInspector] public float speed = 10f;
	[HideInInspector] public int damage = 1;
	[SerializeField] protected string targetTag = "Enemy";
	public GameObject destroyPartciles;

	protected SpriteRenderer sprite { get; set; }
	protected Rigidbody2D rb { get; set; }


	protected virtual void Start()
	{
		sprite = GetComponentInChildren<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
	}

	protected virtual void Update()
	{
		MoveProjectile();
	}

	protected virtual void MoveProjectile() => rb.velocity = sprite.transform.up * speed;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == targetTag)
		{
			OnHitTarget(collision);
			DestroyProjectile();
		}
	}

	protected virtual void DestroyProjectile()
	{
		Instantiate(destroyPartciles, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	protected virtual void OnHitTarget(Collider2D target) { }
}
