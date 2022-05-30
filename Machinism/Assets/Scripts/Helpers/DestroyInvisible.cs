using UnityEngine;

public class DestroyInvisible : MonoBehaviour
{
	bool visible;

	[SerializeField] private float timeToDestroy;
	[SerializeField] private float timeToDestroyWhenNeverVisible;
	float timer;


	private void Start() => timer = timeToDestroyWhenNeverVisible;

	private void OnBecameInvisible()
	{
		visible = false;
		timer = timeToDestroy;
	}

	private void OnBecameVisible()
	{
		visible = true;
	}

	private void Update()
	{
		if (!visible)
		{
			timer -= Time.deltaTime;

			if (timer < 0)
			{
				Destroy(transform.root.gameObject);
			}
		}
	}
}
