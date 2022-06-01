using System.Collections;

public class RocketShip : EnemySpaceship
{
	protected override void OnDeath()
	{
		maxCoinAmount = 2;
		circleUnitSize = .4f;

		base.OnDeath();
	}

	public override IEnumerator Shoot()
	{
		CreateProjectile();

		return base.Shoot();
	}
}
