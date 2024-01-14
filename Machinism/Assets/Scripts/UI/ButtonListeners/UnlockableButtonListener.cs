using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UnlockableButtonListener : ButtonListener
{
	[SerializeField] private int cost;

	public UnityEvent clickedWithNotEnoughMoney;
	public UnityEvent clickedAndUnlocked;
	public UnityEvent clickedOnUnlocked;

	bool isUnlocked;


	protected override void Awake()
	{
		base.Awake();

		// removes all previous button behaviour that was set in the inspector
		button.onClick = null;
	}

	protected override void NewListener()
	{
		if (GemCurrency.instance.value >= cost && !isUnlocked)
		{
			isUnlocked = true;
			clickedAndUnlocked.Invoke();
		}
		else if (isUnlocked)
		{
			clickedOnUnlocked.Invoke();
		}
		else
		{
			clickedWithNotEnoughMoney.Invoke();
		}
	}
}
