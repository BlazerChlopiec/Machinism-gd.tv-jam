using TMPro;

public class GemUIText : UIText
{
	private void Update() => field.text = GemCurrency.instance.value.ToString();
}
