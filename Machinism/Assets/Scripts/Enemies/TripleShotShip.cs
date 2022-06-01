using System.Collections;
using UnityEngine;

public class TripleShotShip : EnemySpaceship
{
	[SerializeField] private float tripleShotInBetween = .1f;

	protected override void OnDeath()
	{
		minCoinAmount = 5;
		maxCoinAmount = 8;
		circleUnitSize = .8f;

		base.OnDeath();
	}

	public override IEnumerator Shoot()
	{
		CreateProjectile();
		yield return new WaitForSeconds(tripleShotInBetween);
		CreateProjectile();
		yield return new WaitForSeconds(tripleShotInBetween);
		CreateProjectile();
		yield return new WaitForSeconds(tripleShotInBetween);

		yield return base.Shoot();
	}
}
