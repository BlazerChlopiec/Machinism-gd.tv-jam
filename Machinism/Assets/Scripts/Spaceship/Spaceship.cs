using EZCameraShake;
using System;
using System.Collections;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
	Rigidbody2D rb;
	SpaceshipCameraController cam;

	//mobile
	FixedJoystick joystick;
	BreaksButton breaksButton;
	//


	[SerializeField] private float acceleration = 400f;
	[SerializeField] private float maxVelocity = 8;
	public float spaceshipScale = 1;

	private float firstFrameAccel;
	private bool breaksInput;

	public Transform rotatedElements;
	public Transform disableOnDestroy;

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
		joystick = FindObjectOfType<FixedJoystick>();
		breaksButton = FindObjectOfType<BreaksButton>();


		firstFrameAccel = acceleration;
	}

	private void Update()
	{


		//smaller ship upgrades
		rotatedElements.localScale = Vector2.one * spaceshipScale;

		if (SystemInfo.deviceType == DeviceType.Desktop)
		{
			RotateToMouse();

			breaksInput = Input.GetKey(KeyCode.LeftShift);
		}
		if (SystemInfo.deviceType == DeviceType.Handheld)
		{
			RotateToJoystick();

			breaksInput = breaksButton.pressed;
		}

		cam.DynamicSize(rb.velocity);

		if (breaksInput)
		{
			acceleration = 0;
			vel = Vector2.Lerp(vel, Vector2.zero, 4 * Time.deltaTime);
		}

		if (!breaksInput)
		{
			acceleration = firstFrameAccel;
		}
	}

	private void RotateToMouse() => rotatedElements.transform.LookAtMouseSmoothly(smoothT: 20);
	private void RotateToJoystick()
	{
		var joystickAxes = new Vector2(joystick.Horizontal, joystick.Vertical);
		if (joystickAxes != Vector2.zero) rotatedElements.transform.rotation = Utils.AxesToZRotSmoothed(rotatedElements.transform.rotation, joystickAxes);
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
		disableOnDestroy.gameObject.SetActive(false);
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
		if (rotatedElements != null) disableOnDestroy.gameObject.SetActive(true);
		isDead = false;
	}
}