using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private Dictionary<string, AudioClip> soundPool;

    private void Awake()
    {
        Instance = this;
        soundPool = new Dictionary<string, AudioClip>();
        LoadAllSounds();
    }

    private void LoadAllSounds()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Sounds/Effect");

        for(int i = 0; i < clips.Length; i++)
        {
            soundPool.Add(clips[i].name, clips[i]);
        }
    }

    public void PlayerOneShot(string soundName)
    {
        if (soundPool == null) return;
        if (soundPool.ContainsKey(soundName) == false) return;
        AudioSource.PlayClipAtPoint(soundPool[soundName], Camera.main.transform.position, 1f);
    }


}
