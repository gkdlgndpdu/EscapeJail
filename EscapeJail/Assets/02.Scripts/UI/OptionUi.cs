using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUi : MonoBehaviour
{
    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private Slider effectSlider;

    [SerializeField]
    private UI_CustomToggle bgmToggle;

    [SerializeField]
    private UI_CustomToggle effectMuteToggle;

    public void MuteBgm(bool OnOff)
    {
        if (SoundManager.Instance == null) return;

        if (OnOff == false)
            PlayerPrefs.SetInt(PlayerPrefKeys.BgmMuteKey,0);
        else if (OnOff == true)
            PlayerPrefs.SetInt(PlayerPrefKeys.BgmMuteKey,1);

        SoundManager.Instance.SetBgmMute();

    }
    public void MuteEffect(bool OnOff)
    {
        if (SoundManager.Instance == null) return;      

        if (OnOff == false)
            PlayerPrefs.SetInt(PlayerPrefKeys.EffectMuteKey, 0);
        else if (OnOff == true)
            PlayerPrefs.SetInt(PlayerPrefKeys.EffectMuteKey, 1);
    }

    public void OnBgmSliderChange()
    {
        if (bgmSlider == null) return;

        PlayerPrefs.SetFloat(PlayerPrefKeys.BgmVolumeKey, bgmSlider.value);
        SoundManager.Instance.SetBgmVolume();
    }
    public void OnEffectSliderChange()
    {
        if (effectSlider == null) return;
        PlayerPrefs.SetFloat(PlayerPrefKeys.EffectVolumeKey, effectSlider.value);
    }

    private void OnEnable()
    {
        TimeManager.Instance.StopTime();

        SetPrefValue();
    }

    private void SetPrefValue()
    {
        if (bgmSlider != null)
        {
            bgmSlider.value = PlayerPrefs.GetFloat(PlayerPrefKeys.BgmVolumeKey, 1f);
        }
        if (effectSlider != null)
        {
            effectSlider.value = PlayerPrefs.GetFloat(PlayerPrefKeys.EffectVolumeKey, 1f);
        }
    }

    private void OnDisable()
    {
        TimeManager.Instance.ResumeTime();
    }
}
