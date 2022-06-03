public class MoneyUIText : UIText
{
	private void Update() => field.text = SpaceshipMoney.value.ToString() + "$";
}
