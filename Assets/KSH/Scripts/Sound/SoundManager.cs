using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioSourceInfo
{
    public AudioClip audio;
    public string name;
}

public class SoundManager : MonoBehaviour
{
    public List<AudioSourceInfo> audiosourceArray = new List<AudioSourceInfo>();
    public AudioSource vfxPlayer;

    public void PlayVfx(string _name)
    {
        AudioSourceInfo tmp = audiosourceArray.Find(o => o.name.Equals(_name));
        vfxPlayer.clip = tmp.audio;
        vfxPlayer.Play();
    }
}
