public class HealthUIText : UIText
{
	private void Update() => field.text = SpaceshipHealth.health.ToString();
}
