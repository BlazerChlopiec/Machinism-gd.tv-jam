using EZCameraShake;
using System;
using System.Collections;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
	Rigidbody2D rb;
	SpaceshipCameraController cam;


	[SerializeField] private float acceleration = 400f;
	[SerializeField] private float maxVelocity = 8;
	public float spaceshipScale = 1;

	private float firstFrameAccel;
	private bool hasStopped;

	public Transform rotatedElements; // basically the first child

	public GameObject deathParticles;

	Vector3 vel;
	public bool isDead;


	private void Start()
	{
		SpaceshipHealth.OnDeath += OnDeath;
		SpaceshipHealth.OnRefil += OnRefil;
		SpaceshipHealth.RefillHealth();

		rb = GetComponent<Rigidbody2D>();
		cam = FindObjectOfType<SpaceshipCameraController>();


		firstFrameAccel = acceleration;
	}

	private void Update()
	{
		rotatedElements.localScale = Vector2.one * spaceshipScale;

		//	RotateToMouse();
		RotateToJoystick();

		cam.DynamicSize(rb.velocity);

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

	private void RotateToMouse() => rotatedElements.transform.LookAtMouseSmoothly(smoothT: 20);
	private void RotateToJoystick()
	{
		var inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		if (inputVector != Vector2.zero)
		{
			var targetRotation = Quaternion.LookRotation(Vector3.forward, inputVector);
			rotatedElements.transform.rotation = Quaternion.Slerp(rotatedElements.transform.rotation, targetRotation, 20 * Time.deltaTime);
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


		//
		DynamicCanvas targetCanvas; // the canvas that is going to be turned on

		//determine the targetCanvas
		var shopCanvas = FindObjectOfType<ShopCanvas>();
		if (shopCanvas.HasAvailableUpgrades()) targetCanvas = shopCanvas;
		else targetCanvas = FindObjectOfType<GameOverCanvas>();
		//

		//finally open the targetCanvas
		targetCanvas.Invoke("Open", .5f);
		//
	}

	private void OnRefil()
	{
		if (rotatedElements != null) rotatedElements.gameObject.SetActive(true);
		isDead = false;
	}
}