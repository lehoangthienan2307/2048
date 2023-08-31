using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SoundType {
    BGM,
    SFX
}

public enum SoundId {
    BGM_1,
    SFX_1,
    SFX_2
}

[System.Serializable]
public class Sound
{
    public SoundType soundType;
    public SoundId soundId;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float audioClipVolume=1f;

    public bool isLoop;

    [HideInInspector]
    public AudioSource source;
}
