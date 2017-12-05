using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource bgmSource;
    private Dictionary<string, AudioClip> soundEffectPool;
    private Dictionary<string, AudioClip> bgmPool;
    private float volume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            soundEffectPool = new Dictionary<string, AudioClip>();
            bgmPool = new Dictionary<string, AudioClip>();
            bgmSource = GetComponent<AudioSource>();
            bgmSource.volume = volume;
            bgmSource.loop = true;
            LoadSounds();

         //   PlayerBgm("Main");

            DontDestroyOnLoad(Instance.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void LoadSounds()
    {
        LoadBGM();
        LoadSoundEffect();
    }

    private void LoadBGM()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Sounds/Bgm");

        for (int i = 0; i < clips.Length; i++)
        {
            bgmPool.Add(clips[i].name, clips[i]);
        }
    }

    private void LoadSoundEffect()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Sounds/Effect");

        for (int i = 0; i < clips.Length; i++)
        {
            soundEffectPool.Add(clips[i].name, clips[i]);
        }
    }

    public void PlayerBgm(string soundName)
    {
        if (bgmSource == null || bgmPool == null) return;
        if (bgmPool.ContainsKey(soundName) == false) return;

        bgmSource.clip = bgmPool[soundName];
        bgmSource.Play();

        StartCoroutine(SlowStartBgm());
    }

    public void EndBgm()
    {
        SlowEndBgm();
    }

    public void ChangeBgm(string name)
    {
        StartCoroutine(BgmChangeRoutine(name));
    }

    IEnumerator BgmChangeRoutine(string name)
    {
        yield return StartCoroutine(SlowEndBgm());
        PlayerBgm(name);
    }

    IEnumerator SlowEndBgm()
    {
        float countVolume = 0f;
        float count = 0f;

        if (bgmSource != null)
            bgmSource.volume = 0f;

        while (true)
        {
            count += Time.deltaTime;
            countVolume = Mathf.Lerp(volume, 0f, count);

            if (bgmSource != null)
                bgmSource.volume = countVolume;
      
            if (count >= 1f)
            {
                countVolume = volume;
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator SlowStartBgm()
    {
        float countVolume = 0f;
        float count = 0f;

        if (bgmSource != null)
            bgmSource.volume = 0f;

        while (true)
        {
            count += Time.deltaTime;
            countVolume = Mathf.Lerp(0f, volume, count);

            if (bgmSource != null)
                bgmSource.volume = countVolume;
       
            if (count >= 1f)
            {
                countVolume = volume;
                yield break;
            }
            yield return null;
        }

    }

    public void PlaySoundEffect(string soundName)
    {
        PlayerOneShot(soundName);
    }

    private void PlayerOneShot(string soundName)
    {
        if (soundEffectPool == null) return;
        if (soundEffectPool.ContainsKey(soundName) == false) return;
        AudioSource.PlayClipAtPoint(soundEffectPool[soundName], Camera.main.transform.position, volume);
    }


    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Return))
    //    {
    //        ChangeBgm("Stage2");
    //    }

    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        ChangeBgm("Stage3");
    //    }
    //}

}
