public class GoldMeteor : Meteor
{
	protected override void OnDeath()
	{
		minCoinAmount = 100;
		maxCoinAmount = 100;
		circleUnitSize = 1 * randomScale;

		base.OnDeath();
	}
}
