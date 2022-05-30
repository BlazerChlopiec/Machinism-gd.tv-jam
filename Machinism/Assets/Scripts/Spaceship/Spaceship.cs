using EZCameraShake;
using System;
using System.Collections;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
	Rigidbody2D rb;
	CameraController cam;
	public SpriteRenderer sprite;


	[SerializeField] private float acceleration = 400f;
	[SerializeField] private float maxVelocity = 8;
	public float spaceshipScale = 1;

	private float firstFrameAccel;
	private bool hasStopped;

	public Transform rotatedElements; // basically the first child

	public GameObject deathParticles;

	Vector3 vel;
	Color startColor;
	public bool isDead;

	bool isOnInvincibilityFrames;


	private void Start()
	{
		SpaceshipHealth.OnDeath += OnDeath;
		SpaceshipHealth.OnRefil += OnRefil;
		SpaceshipHealth.RefillHealth();

		rb = GetComponent<Rigidbody2D>();
		cam = FindObjectOfType<CameraController>();

		startColor = sprite.color;

		firstFrameAccel = acceleration;
	}

	private void Update()
	{
		rotatedElements.localScale = Vector2.one * spaceshipScale;
		rotatedElements.transform.LookAtMouseSmoothly(smoothT: 20);
		cam.DynamicSize(rb.velocity);


		if (Timers.IsUp("SpaceshipInvFrames") && isOnInvincibilityFrames)
		{
			OnInvincibilityFramesStop();
		}

		if (Input.GetKey(KeyCode.LeftShift)) hasStopped = true;
		if (!Input.GetKey(KeyCode.LeftShift)) hasStopped = false;

		if (hasStopped)
		{
			acceleration = 0;
			vel = Vector2.Lerp(vel, Vector2.zero, 4 * Time.deltaTime);
		}

		if (!hasStopped)
		{
			acceleration = firstFrameAccel;
		}
	}

	private void FixedUpdate()
	{
		if (!isDead)
		{
			//rotatedElements is facing the mouse every frame
			vel += rotatedElements.transform.up * acceleration;
			vel = Vector3.ClampMagnitude(vel, maxVelocity);
			rb.velocity = vel;
		}
		else
		{
			vel = Vector3.zero;
			rb.velocity = Vector3.zero;
		}
	}


	private void OnDeath()
	{
		rotatedElements.gameObject.SetActive(false);
		Instantiate(deathParticles, transform.position, Quaternion.identity);
		isDead = true;

		CameraShaker.Instance.ShakeOnce(6f, 8, 0, 1);

		var shopCanvas = FindObjectOfType<ShopCanvas>();
		if (shopCanvas.HasAnyMoreUpgrades()) shopCanvas.Invoke("OpenShop", .5f);
		else
		{
			var gameOverCanvas = FindObjectOfType<GameOverCanvas>();
			gameOverCanvas.Invoke("Show", .5f);
		}
	}

	private void OnRefil()
	{
		if (rotatedElements != null) rotatedElements.gameObject.SetActive(true);
		isDead = false;
	}

	public void OnInvincibilityFramesStart()
	{
		isOnInvincibilityFrames = true;

		var col = sprite.color;
		col.a = .3f;
		sprite.color = col;
	}

	public void OnInvincibilityFramesStop()
	{
		isOnInvincibilityFrames = false;
		sprite.color = startColor;
	}
}