using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCanvas : MonoBehaviour
{
	public GameObject shopContainer;


	private void Start() => shopContainer.SetActive(false);

	public void OpenShop()
	{
		Time.timeScale = 0;

		shopContainer.SetActive(true);

		foreach (var item in GetComponentsInChildren<UpgradeButton>())
		{
			item.UpdateInteractability();
			item.UpdateData();
		}
	}

	public void DisableAllUpgradeButtons()
	{
		foreach (var item in GetComponentsInChildren<UpgradeButton>())
		{
			item.GetComponent<Button>().interactable = false;
		}
	}

	public bool HasAnyMoreUpgrades()
	{
		var hasAvailableUpgrades = false;
		var upgrades = FindObjectsOfType<UpgradeButton>(includeInactive: true);

		foreach (var x in upgrades)
		{
			if (x.levelTarget.currentLevel != x.levelTarget.levels.Count && x.levelTarget.levels[x.levelTarget.currentLevel].cost <= Money.value)
			{
				hasAvailableUpgrades = true;
			}
			if (hasAvailableUpgrades) break;
		}

		return hasAvailableUpgrades;
	}

	public void CloseShop()
	{
		Time.timeScale = 1;

		shopContainer.SetActive(false);
		Timers.New("SpaceshipInvFrames", 2);
		SpaceshipHealth.RefillHealth();
		FindObjectOfType<Spaceship>().OnInvincibilityFramesStart();
	}
}
