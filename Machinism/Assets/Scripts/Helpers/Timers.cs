using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timers : MonoSingleton<Timers>
{
	static List<Counter> timeCounters = new List<Counter>();

	private void Update()
	{
		foreach (var item in timeCounters)
		{
			item.time -= Time.deltaTime;
		}
	}
	public static void New(string name, float time)
	{
		Counter counter = new Counter
		{
			name = name,
			time = time
		};

		if (!AlreadyExists(counter))
			timeCounters.Add(counter);

		if (AlreadyExists(counter))
		{
			//overriding the counter
			Counter target = timeCounters[timeCounters.IndexOf(timeCounters.Find(x => x.name == name))];
			target.stop = false;
			target.time = time;
		}
	}

	public static void Clear(string name)
	{
		Counter target = timeCounters[timeCounters.IndexOf(timeCounters.Find(x => x.name == name))];
		target.time = -10f;
		target.stop = true;
	}

	public static bool IsUp(string name, bool trueIfDoesntExist = true)
	{
		var result = timeCounters.Find(x => x.name == name);
		if (result == null) return trueIfDoesntExist; // very convinient THO i would've lost an interview
		return result.time < 0;
	}

	private static bool AlreadyExists(Counter counter)
	{
		List<Counter> temp = new List<Counter>();
		temp = timeCounters.FindAll(x => x.name == counter.name);

		if (temp.Count == 0)
			return false;
		else
			return true;
	}
}

public class Counter
{
	public string name;
	public float time;
	public bool stop;
}
