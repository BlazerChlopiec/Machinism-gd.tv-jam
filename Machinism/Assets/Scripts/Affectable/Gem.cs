using UnityEngine;

public class Gem : Collectable
{
	public override void OnTouch()
	{
		GemCurrency.instance.Add(1);

		if (collectParticles != null) Instantiate(collectParticles, transform.position, Quaternion.identity);

		AudioManager.instance.Play("CoinCollect");

		Destroy(gameObject);
	}
}
