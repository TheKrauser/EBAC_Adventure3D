using UnityEngine.Audio;
using System;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public SoundClass[] sounds;
    public AudioMixer mixer;

    public static AudioManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (SoundClass s in sounds)
        {
            s.audioS = gameObject.AddComponent<AudioSource>();
            s.audioS.clip = s.clip;
            s.audioS.volume = s.volume;
            s.audioS.pitch = s.pitch;
            s.audioS.loop = s.loop;
        }
    }

    public void PlaySound(string soundName)
    {
        SoundClass s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
            return;

        s.audioS.PlayOneShot(s.audioS.clip);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleVolume();
        }
    }

    private bool isMuted = false;
    private void ToggleVolume()
    {
        isMuted = !isMuted;
        
        var s = GameObject.FindObjectsOfType<AudioSource>();
        foreach(var sound in s)
        {
            sound.mute = isMuted;
        }
    }
}


[System.Serializable]
public class SoundClass
{
    public string name;

    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource audioS;
}
