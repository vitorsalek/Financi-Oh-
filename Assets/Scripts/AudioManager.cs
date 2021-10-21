using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	//public AudioMixerGroup mixerGroup;

	public Sound[] sounds;
	public static AudioManager current;
	void Awake()
	{
		if (current == null)
			current = this;
		else
			Destroy(gameObject);
		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = s.mixerGroup;
		}
	}
    private void Start()
    {
		GameObject[] objs = GameObject.FindGameObjectsWithTag("persistentData");
		if (objs.Length > 1)
		{
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
		Play("ThemeMusic");
	}
    public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + sound + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

}
