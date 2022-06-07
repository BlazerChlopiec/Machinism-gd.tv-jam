using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoSingleton<AudioManager>
{
	public List<Sound> sounds = new List<Sound>();

	public List<string> startSounds = new List<string>();

	protected void Start()
	{
		foreach (var sound in sounds)
		{
			sound.source = gameObject.AddComponent<AudioSource>();

#if UNITY_EDITOR
			UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(sound.source, false); // close inspector on initiation
#endif
		}

		foreach (var name in startSounds)
		{
			Play(name);
		}
	}

	public void Play(string name)
	{
		var s = GetSound(name);

		if (s == null) return;

		s.source.volume = s.volume;
		s.source.pitch = s.pitch + UnityEngine.Random.Range(-s.randomPitchFactor, s.randomPitchFactor);
		s.source.loop = s.loop;
		s.source.clip = s.clips[UnityEngine.Random.Range(0, s.clips.Count)];

		if (s.clips.Count > 0)
			s.source.Play();
		else
			Debug.LogError("There are no clips in '" + s.name + "' Sound");
	}

	public void Stop(string name)
	{
		var s = GetSound(name);

		if (s == null) return;

		s.source.Stop();
	}

	public float GetCustomVar(string soundName, int index)
	{
		var s = GetSound(soundName);

		if (s == null) return 0f;

		if (s.customVariables.Count == 0)
		{
			Debug.LogError("There are no customVariables in '" + soundName + "' !!!");
		}
		return s.customVariables[0];
	}

	private Sound GetSound(string name)
	{
		name = name.ToLower();
		name = name.Replace(" ", "");
		var s = sounds.Find(x => x.name.ToLower() == name);

		if (s == null)
		{
			Debug.LogError("There is no sound of the name - '" + name + "' !!!");
		}

		return s;
	}
}

[System.Serializable]
public class Sound
{
	public string name;

	[HideInInspector] public AudioSource source;

	public List<AudioClip> clips = new List<AudioClip>();
	public List<float> customVariables = new List<float>();

	[Range(0, 3)] public float volume = 1;
	[Range(0, 3)] public float pitch = 1;
	[Range(0, 3)] public float randomPitchFactor = 0;

	public bool loop;
}