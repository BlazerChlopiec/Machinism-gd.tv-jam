public class NormalMeteor : Meteor
{
	protected override void OnDeath()
	{
		maxCoinAmount = 3;
		circleUnitSize = 1 * randomScale;

		base.OnDeath();
	}
}
