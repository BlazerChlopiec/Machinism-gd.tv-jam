using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCanvas : DynamicCanvas
{
	public override void Open()
	{
		base.Open();

		Time.timeScale = 0;

		foreach (var item in GetComponentsInChildren<UpgradeButton>())
		{
			item.UpdateInteractability();
			item.UpdateData();
		}
	}

	public override void Close()
	{
		base.Close();

		Time.timeScale = 1;

		Timers.New("SpaceshipInvFrames", 2);
		SpaceshipHealth.RefillHealth();
		FindObjectOfType<SpaceshipColors>().OnInvincibilityFramesStart();
	}

	public void DisableAllUpgradeButtons()
	{
		foreach (var item in GetComponentsInChildren<UpgradeButton>())
		{
			item.GetComponent<Button>().interactable = false;
		}
	}

	public bool HasAvailableUpgrades()
	{
		var hasAvailableUpgrades = false;
		var upgrades = FindObjectsOfType<UpgradeButton>(includeInactive: true);

		foreach (var x in upgrades)
		{
			if (x.levelTarget.currentLevel != x.levelTarget.levels.Count && x.levelTarget.levels[x.levelTarget.currentLevel].cost <= SpaceshipMoney.value)
			{
				hasAvailableUpgrades = true;
			}
			if (hasAvailableUpgrades) break;
		}

		return hasAvailableUpgrades;
	}
}
