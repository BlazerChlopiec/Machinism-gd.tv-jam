using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform player;
	public Camera cam { get; private set; }
	public CameraShaker shaker { get; private set; }

	[SerializeField] private float minSize;
	[SerializeField] private float maxSize;

	Spaceship spaceship;


	private void Start()
	{
		spaceship = FindObjectOfType<Spaceship>();
		cam = GetComponent<Camera>();
	}

	private void Update()
	{
		if (spaceship != null && !spaceship.isDead)
		{
			Vector3 target = Vector2.Lerp(transform.position, player.position, 10 * Time.deltaTime);
			target.z = -10;
			transform.position = target;
		}
	}

	public void DynamicSize(Vector2 speed)
	{
		float target = Mathf.Clamp(minSize + speed.magnitude / 3, minSize, maxSize);
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, target, 3 * Time.deltaTime);
	}
}
