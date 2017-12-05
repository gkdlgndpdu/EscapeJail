using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class EachSound : MonoBehaviour
{
    private AudioSource audioSource;

    public void Initialize(AudioClip clip,float volume =1f)
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        if (audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
        }
    }

    public void Update()
    {
        if(audioSource!=null)
        {
            if (audioSource.isPlaying == false)
                this.gameObject.SetActive(false);
        }
    }

}
