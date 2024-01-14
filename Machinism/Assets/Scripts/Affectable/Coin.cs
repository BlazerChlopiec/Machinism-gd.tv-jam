using UnityEngine;

public class Coin : Collectable
{
	public override void OnTouch()
	{
		MoneyCurrency.instance.Add(10);

		if (collectParticles != null) Instantiate(collectParticles, transform.position, Quaternion.identity);

		AudioManager.instance.Play("CoinCollect");

		Destroy(gameObject);
	}
}
