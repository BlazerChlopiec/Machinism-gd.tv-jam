using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using MyDebug;

namespace MyDebug
{
	public class DebugViewer : MonoBehaviour
	{
		public static bool showData { get; private set; }

		[SerializeField] private KeyCode openKey = KeyCode.Tab;
		[Space(5)]

		[Header("All targets are case sensitive!")]
		[Header("All targets can't have namespaces!")]
		[Space(5)]

		[SerializeField] private DebugStringSettings debugStringSettings;
		[SerializeField] private DebugEvent events;
		[SerializeField] private List<DebugFloat> floats;
		[SerializeField] private List<DebugButton> buttons;

		static private List<DebugString> debugStrings = new List<DebugString>();


		GUIStyle style;
		private int defaultFontSize = 20;

		private void Start()
		{
			style = new GUIStyle();
			style.normal.textColor = Color.white;
			style.fontSize = defaultFontSize;

			foreach (var item in floats)
			{
				// THESE HAVE TO BE CLEARED BECAUSE THEY GET SAVED AND SERIALIZED BY UNITY WHICH IS BAD
				item.fields.Clear();
				item.maxValueForSlider.Clear();


				// Get the properties of 'Type' class object.
				item.fields = Type.GetType(item.typeName).GetFields().ToList();
				var targetComponent = GetTargettedComponent(item);

				foreach (var field in item.fields.ToArray())
				{
					if (field.GetValue(GetTargettedComponent(item)) is float)
					{
						// if value is float
					}
					else
					{
						// if value isn't float
						item.fields.Remove(field);
						continue;
					}

					var itemValue = (float)field.GetValue(targetComponent);
					var temp = itemValue * item.sliderSizeMulti;

					item.maxValueForSlider.Add(temp);
				}
			}
		}

		private Component GetTargettedComponent(DebugFloat target) => target.typeHolder.GetComponent(target.typeName);

		public static void AddString(string contents, string value, Color color)
		{
			if (!showData) return;

			if (color == Color.clear) color = Color.black;

			var temp = new DebugString
			{
				contents = contents,
				value = value,
				color = color
			};

			if (debugStrings.Find(x => x.contents == temp.contents) == null)
				debugStrings.Add(temp);
			else
			{
				debugStrings.Find(x => x.contents == temp.contents).value = temp.value;
			}
		}

		private void Update()
		{
			if (Input.GetKeyDown(openKey))
			{
				if (showData == false) events.OnOpen.Invoke();
				else events.OnExit.Invoke();

				showData = !showData;
			}
		}

		private void OnGUI()
		{
			if (!showData) return;

			style.normal.textColor = Color.white;
			style.fontSize = defaultFontSize;

			// floats
			foreach (var item in floats)
			{
				var targetComponent = GetTargettedComponent(item);

				int fieldNameLabelXOffset = 200;
				int valueLabelXOffset = 20;
				int fieldDistance = 30;
				int sliderWidth = 100;

				for (int i = 0; i < item.fields.Count; i++)
				{
					var value = (float)item.fields[i].GetValue(targetComponent);

					GUI.Label(new Rect(fieldNameLabelXOffset + item.offset.x, (fieldDistance * i) + item.offset.y, 20, 20), " - " + item.fields[i].Name, style);
					item.fields[i].SetValue(targetComponent, GUI.HorizontalSlider(new Rect(20 + item.offset.x, (fieldDistance * i) + item.offset.y, sliderWidth, 20), value, 0, item.maxValueForSlider[i]));
					GUI.Label(new Rect(sliderWidth + valueLabelXOffset + item.offset.x, (fieldDistance * i) + item.offset.y, 20, 20), " - " + value, style);
				}
			}

			//buttons
			foreach (var item in buttons)
			{
				if (GUI.Button(new Rect(100 + item.offset.x, item.offset.y, 10 * item.buttonLabel.Length, 20), item.buttonLabel))
					item.OnClick.Invoke();
			}

			//debug strings
			for (int i = 0; i < debugStrings.Count; i++)
			{
				style.fontSize = debugStringSettings.fontSize;
				style.normal.textColor = debugStrings[i].color;

				string label = debugStrings[i].contents + debugStrings[i].value;

				GUI.Label(new Rect(debugStringSettings.offset.x, debugStringSettings.offset.y + (debugStringSettings.spaceBetweenStrings * i), 20, 20), label, style);
			}
		}

		private void OnDisable()
		{
			foreach (var item in floats)
			{
				item.fields.Clear();
				item.maxValueForSlider.Clear();
			}
		}
	}
	[Serializable]
	public class DebugFloat
	{
		public string typeName = "Rigidbody";
		public GameObject typeHolder;
		public Vector2 offset;
		public float sliderSizeMulti = 3;

		[HideInInspector] public List<FieldInfo> fields = new List<FieldInfo>();
		[NonSerialized] public List<float> maxValueForSlider = new List<float>();
	}

	[Serializable]
	public class DebugButton
	{
		public string buttonLabel = "Click Me!";
		public Vector2 offset;
		public UnityEvent OnClick;
	}

	[Serializable]
	public class DebugEvent
	{
		public UnityEvent OnOpen;
		public UnityEvent OnExit;
	}

	[Serializable]
	public class DebugString
	{
		public string contents;
		public string value;
		public Color color;
	}

	[Serializable]
	public class DebugStringSettings
	{
		public Vector2 offset;
		public int fontSize = 30;
		public int spaceBetweenStrings = 20;
	}
}