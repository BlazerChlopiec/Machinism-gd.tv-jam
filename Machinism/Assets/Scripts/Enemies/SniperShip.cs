using System.Collections;
using UnityEngine;

public class SniperShip : EnemySpaceship
{
	LineRenderer line;


	protected override void Start()
	{
		line = GetComponentInChildren<LineRenderer>();

		base.Start();
	}

	protected override void Update()
	{
		base.Update();

		if (!transform.IsSeenByCamera()) line.enabled = false;
		else line.enabled = true;
		line.SetPosition(1, spaceship.transform.position - transform.position);
	}

	protected override void OnDeath()
	{
		maxCoinAmount = 2;
		circleUnitSize = .4f;

		base.OnDeath();
	}

	public override IEnumerator Shoot(float shootFreq)
	{
		CreateProjectile();
		yield return new WaitForSeconds(.8f);

		yield return base.Shoot(shootFrequency - .8f);
	}
}
