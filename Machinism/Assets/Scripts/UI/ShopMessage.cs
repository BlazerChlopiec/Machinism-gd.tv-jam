using TMPro;
using UnityEngine;

public class ShopMessage : MonoBehaviour
{
	public GameObject messageContainer;

	public RectTransform restPosition;
	public RectTransform shownPosition;

	[SerializeField] private LeanTweenType easing;

	TextMeshProUGUI text;

	float cheapestTier;
	bool hasShownMessage;


	private void Start()
	{
		text = messageContainer.GetComponentInChildren<TextMeshProUGUI>();

		GetCheapestTier();
	}

	public void GetCheapestTier(bool hasShownMessage = false)
	{
		this.hasShownMessage = hasShownMessage;

		var upgrades = FindObjectsOfType<UpgradeButtonListener>(includeInactive: true);

		cheapestTier = int.MaxValue;
		foreach (var item in upgrades)
		{
			if (item.levelTarget.currentLevel == item.levelTarget.levels.Count) continue;

			var cost = item.levelTarget.levels[item.levelTarget.currentLevel].cost;
			if (cost < cheapestTier)
			{
				cheapestTier = cost;
			}
		}
	}

	private void Update()
	{
		if (MoneyCurrency.instance.value >= cheapestTier && !hasShownMessage)
		{
			hasShownMessage = true;
			ShowMessage("Upgrades are available!");
		}
	}

	public void ShowMessage(string message)
	{
		messageContainer.SetActive(true);

		text.text = message;

		LeanTween.moveY(messageContainer.GetComponent<RectTransform>(), shownPosition.anchoredPosition.y, .4f)
			.setEase(easing)
			.setOnComplete(StartRepeating);

		AudioManager.instance.Play("UpgradesAreAvailable");
	}

	private void StartRepeating() => Invoke(nameof(Repeat), 2f);

	private void Repeat()
	{
		LeanTween.moveY(messageContainer.GetComponent<RectTransform>(), restPosition.anchoredPosition.y, .4f)
			.setEase(easing);
	}
}
