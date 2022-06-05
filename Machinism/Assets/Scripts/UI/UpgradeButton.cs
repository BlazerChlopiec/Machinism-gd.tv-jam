using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UpgradeButton : MonoBehaviour
{
	[Header("The value you want to change has to be public!")]
	public LevelTarget levelTarget;

	Button button;
	TextMeshProUGUI text;



	private void OnEnable()
	{
		button = GetComponent<Button>();
		text = button.GetComponentInChildren<TextMeshProUGUI>();
	}


	public void UpdateInteractability()
	{
		if (levelTarget.currentLevel == levelTarget.levels.Count) // if already bought all upgradese
		{
			button.interactable = false;
		}
		else if (SpaceshipMoney.value < levelTarget.levels[levelTarget.currentLevel].cost) // if no money
		{
			button.interactable = false;
		}
		else // if can buy
		{
			button.interactable = true;
		}
	}

	public void UpdateData() // its called from a parent so it can update without being enabled
	{
		if (levelTarget.currentLevel == levelTarget.levels.Count) // if already bought all upgradese
		{
			text.text = "MAX";
			return;
		}
		else if (SpaceshipMoney.value < levelTarget.levels[levelTarget.currentLevel].cost) // if no money
		{
			text.text = levelTarget.levels[levelTarget.currentLevel].levelName + " - "
					  + levelTarget.levels[levelTarget.currentLevel].cost + "$";
			return;
		}
		else // if can buy
		{
			text.text = levelTarget.levels[levelTarget.currentLevel].levelName + " - "
					  + levelTarget.levels[levelTarget.currentLevel].cost + "$";
		}
	}

	public void Upgrade()
	{
		SpaceshipMoney.Remove(levelTarget.levels[levelTarget.currentLevel].cost);

		var targetComponent = GetTargettedComponent();

		FieldInfo field = Type.GetType(levelTarget.typeName).GetField(levelTarget.floatName);
		field.SetValue(targetComponent, levelTarget.levels[levelTarget.currentLevel].value);

		levelTarget.currentLevel++;
		UpdateData();

		FindObjectOfType<ShopCanvas>().DisableAllUpgradeButtons();
		AudioManager.instance.Play("Buy");
	}

	private Component GetTargettedComponent() => levelTarget.typeHolder.GetComponent(levelTarget.typeName);
}


[Serializable]
public class LevelTarget
{
	public GameObject typeHolder;
	public string typeName;
	public string floatName;

	[Space(5)]
	public List<Level> levels;
	[HideInInspector]
	public int currentLevel;
}

[Serializable]
public class Level
{
	public int cost;
	public float value;
	public string levelName;
}
