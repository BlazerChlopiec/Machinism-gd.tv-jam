public class Money : UIText
{
	public static int value { get; protected set; }


	public static void Add(int amount) => value += amount;
	public static void Remove(int amount) => value -= amount;

	protected override void ApplyValue()
	{
		base.ApplyValue();

		field.text = value.ToString() + "$";
	}
}
