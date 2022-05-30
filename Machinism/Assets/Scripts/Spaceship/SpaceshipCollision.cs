using UnityEngine;

public class SpaceshipCollision : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.CompareTag("Affectable"))
		{
			collision.transform.GetComponent<Affectable>().OnTouch();
		}

		if (collision.transform.CompareTag("Enemy"))
		{
			if (Timers.IsUp("SpaceshipInvFrames"))
			{
				SpaceshipHealth.Kill();
				collision.GetComponent<Enemy>().TakeDamage(100);
			}
		}
	}
}
