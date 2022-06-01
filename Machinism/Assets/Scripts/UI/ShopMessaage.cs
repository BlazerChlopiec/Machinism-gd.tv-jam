using System.Collections;
using TMPro;
using UnityEngine;

public class ShopMessaage : MonoBehaviour
{
	public GameObject messageContainer;

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

		var upgrades = FindObjectsOfType<UpgradeButton>(includeInactive: true);

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
		if (SpaceshipMoney.value >= cheapestTier && !hasShownMessage)
		{
			hasShownMessage = true;
			ShowMessage("Upgrades are available!");
		}
	}

	public void ShowMessage(string message)
	{
		messageContainer.SetActive(true);

		text.text = message;
		LeanTween.moveY(messageContainer.GetComponent<RectTransform>(), 5, .4f)
			.setEase(easing)
			.setOnComplete(StartRepeating);

		AudioManager.instance.Play("UpgradesAreAvailable");
	}

	private void StartRepeating() => StartCoroutine(Repeat());

	private IEnumerator Repeat()
	{
		yield return new WaitForSeconds(2);
		LeanTween.moveY(messageContainer.GetComponent<RectTransform>(), -100, .4f)
			.setEase(easing);
	}
}
