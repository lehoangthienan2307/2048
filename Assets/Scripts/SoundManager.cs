using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<Sound> sounds;
    private void Awake()
    {
        LoadSounds();
    }
    private void LoadSounds()
    {
        // Load BGM sound
        foreach (Sound s in sounds)
        {   
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.isLoop;
            //s.source.outputAudioMixerGroup = s.group;           
        }
    }
    public void Play(SoundId name)
    {
        Sound s = sounds.Find(sound => sound.soundId == name);;
        if (s == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            return;
        }

        
        if (s.soundType == SoundType.BGM)
        {
                s.source.Play();
        }
        else
        {
                s.source.PlayOneShot(s.clip);
        }      
    }

}
