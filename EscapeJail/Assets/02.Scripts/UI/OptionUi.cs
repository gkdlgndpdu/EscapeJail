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
    private Dropdown dropDown;


    [SerializeField]
    private JoyStick moveStick;

    private void Awake()
    {
        if (dropDown != null)
        {
            int language = PlayerPrefs.GetInt(PlayerPrefKeys.LanguageKey, (int)LanguageType.Korean);

            if (language == 0)
            {
                dropDown.value = 0;

            }
            else if (language == 1)
            {
                dropDown.value = 1;
            }
        }
    }

    public void DropDownChangeValue(int level)
    {
        //한글
        if (level == 0)
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.LanguageKey, (int)LanguageType.Korean);
            //한글처리
            Language.Instance.ChangeAllTexts(LanguageType.Korean);
        }
        //영어
        else if (level == 1)
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.LanguageKey, (int)LanguageType.English);
            //영어처리
            Language.Instance.ChangeAllTexts(LanguageType.English);
        }
    }

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

    public void ChangeMoveStickType(bool OnOff)
    {
        if (moveStick == null) return;


        if (OnOff == false)
        {
            moveStick.NowStickType = StickType.UnFixed;
            PlayerPrefs.SetInt(PlayerPrefKeys.MoveStickTypeKey, 1);
        }
        else if (OnOff == true)
        {
            moveStick.NowStickType = StickType.Fixed;
            PlayerPrefs.SetInt(PlayerPrefKeys.MoveStickTypeKey, 0);
        }
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
        if (TimeManager.Instance!=null)
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
        if (TimeManager.Instance!=null)
        TimeManager.Instance.ResumeTime();
    }

    public void ReStartGame()
    {
        SceneManager.Instance.ChangeScene(SceneName.GameScene);
        if (TimeManager.Instance != null)
            TimeManager.Instance.ResumeTime();
    }
    public void GoMenu()
    {
        SceneManager.Instance.ChangeScene(SceneName.MenuScene);
        if (TimeManager.Instance != null)
            TimeManager.Instance.ResumeTime();
    }
}
