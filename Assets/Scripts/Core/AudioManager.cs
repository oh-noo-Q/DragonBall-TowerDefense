using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;

    // public static AudioManager instance;

    void Awake()
    {
        //if (PlayerPrefsManager.Sound == 0) AudioListener.volume = 0f;
        //if (PlayerPrefsManager.Sound == 1) AudioListener.volume = 1f;

        // if (instance == null)
        // {
        //     instance = this;
        // }
        // else
        // {
        //     Destroy(gameObject);
        //     return;
        // }

        // DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(EventID.Mute, MuteSound);
    }

    private void MuteSound(object param)
    {
        var mute = !(bool)param;
        if (mute)
        {
            Stop("BGM");
        }
        else
        {
            Play("BGM");
        }
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventID.Mute, MuteSound);
    }

    public void Play(string name)
    {
        if (!UserData.SoundSetting) return;
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            //Debug.LogWarning($"Sound: {name} not found!");
            return;
        }

        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }

        s.source.Stop();
    }

    public override void OnDestroy()
    {
        // base.OnDestroy();
    }

}