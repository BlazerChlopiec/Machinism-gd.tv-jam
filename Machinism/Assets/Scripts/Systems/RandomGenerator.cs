using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomGenerator
{
	public class RandomGenerator : MonoBehaviour
	{
		Spaceship spaceship;

		[Header("There should be only two children to a Row!")]
		public List<Transform> rows;
		[Space(10)]
		public List<RandomObjects> randomObjects;
		[Space(10)]
		public List<TimeOverride> timeOverrides;
		[Space(10)]


		public Transform rotatedElements;


		private void Start()
		{
			spaceship = FindObjectOfType<Spaceship>();

			foreach (var item in randomObjects)
			{
				// timeBetweenSpawns is constant inspector value, but currentTimeBetweenSpawns can be modified
				item.currentTimeBetweenSpawns = item.timeBetweenSpawns;
			}
		}

		private void Update()
		{
			if (spaceship != null)
			{
				transform.position = spaceship.transform.position;
				rotatedElements.rotation = spaceship.rotatedElements.transform.rotation;
			}

			//managing timeOverrides 
			foreach (var timeOverride in timeOverrides)
			{
				if (Time.timeSinceLevelLoad > timeOverride.triggerOnSeconds)
				{
					foreach (var statOverride in timeOverride.overrides)
					{
						if (!timeOverride.alreadyTriggered)
						{
							var target = randomObjects.Find(x => x.elementName == statOverride.elementName);

							// apply new values
							if (target != null)
							{
								target.currentTimeBetweenSpawns = statOverride.newTimeBetweenSpawns;
								target.enabled = statOverride.enabled;
							}
						}
					}

					timeOverride.alreadyTriggered = true;
				}
			}

			//spawning objects
			foreach (var item in randomObjects)
			{
				if (!item.enabled) return;

				if (Timers.IsUp(item.prefab.name)) // some random name to differentiate the Timers
				{
					Timers.New(item.prefab.name, item.currentTimeBetweenSpawns);
					Instantiate(item.prefab, GetChildPosInRandomRow(item.targetRow), Quaternion.identity);
				}
			}
		}

		private Vector3 GetChildPosInRandomRow(Transform customTargetRow = null)
		{
			Transform targetRow;

			// the item can exclude other rows and only leave one targetRow
			if (customTargetRow == null) targetRow = rows.GetRandom();
			else targetRow = customTargetRow;

			List<Vector3> childrenPos = new List<Vector3>();
			foreach (Transform children in targetRow)
			{
				childrenPos.Add(children.position);
			}

			var index = UnityEngine.Random.Range(0, childrenPos.Count);

			// draw a position from one random children
			return new Vector2(UnityEngine.Random.Range(childrenPos[index].x, childrenPos[index].x),
							   UnityEngine.Random.Range(childrenPos[index].y, childrenPos[index].y));
		}
	}


	[Serializable]
	public class RandomObjects
	{
		public string elementName = "element";
		[Space(5)]

		[Header("null - every row")]
		public Transform targetRow = null;
		[Space(5)]

		public GameObject prefab;
		[Space(5)]

		public float timeBetweenSpawns = 1;
		public bool enabled = true;
		[HideInInspector] public float currentTimeBetweenSpawns;
	}

	[Serializable]
	public class TimeOverride
	{
		public float triggerOnSeconds;
		public List<StatOverride> overrides;
		public bool alreadyTriggered;
	}

	[Serializable]
	public class StatOverride
	{
		public string elementName = "element";
		[Space(5)]

		public float newTimeBetweenSpawns = 1;
		public bool enabled = true;
	}
}