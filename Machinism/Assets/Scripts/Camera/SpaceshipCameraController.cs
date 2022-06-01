using EZCameraShake;
using UnityEngine;

public class SpaceshipCameraController : MonoBehaviour
{
	public Camera cam { get; private set; }
	public CameraShaker shaker { get; private set; }

	[SerializeField] private float minSize = 8;
	[SerializeField] private float maxSize = 13;

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
			Vector3 modifiedTarget = Vector2.Lerp(transform.position, spaceship.transform.position, 10 * Time.deltaTime);
			modifiedTarget.z = -10;
			transform.position = modifiedTarget;
		}
	}

	public void DynamicSize(Vector2 speed)
	{
		float target = Mathf.Clamp(minSize + speed.magnitude / 3, minSize, maxSize);
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, target, 3 * Time.deltaTime);
	}
}
