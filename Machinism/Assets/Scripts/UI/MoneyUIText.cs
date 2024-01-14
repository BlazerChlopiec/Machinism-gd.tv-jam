public class MoneyUIText : UIText
{
	private void Update() => field.text = MoneyCurrency.instance.value.ToString() + "$";
}
