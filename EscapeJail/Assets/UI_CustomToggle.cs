using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public enum ToggleType
{
    Bgm,
    Effect,
    JoyStick
}
[System.Serializable]
public class MyBoolEvent : UnityEvent<bool>
{
}

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(EventTrigger))]
public class UI_CustomToggle : MonoBehaviour
{
    [SerializeField]
    private ToggleType toggleType;

    private bool isOn = true;

    [SerializeField]
    private Sprite onImage;
    [SerializeField]
    private Sprite offImage;

    private Image toggleImage;

    public MyBoolEvent myBoolEvent;



    private void Awake()
    {
        toggleImage = GetComponent<Image>();

      
    }

    private void Start()
    {
        switch (toggleType)
        {
            case ToggleType.Bgm:
                {
                    if (SoundManager.Instance.IsBgmMute == true)
                        isOn = false;
                    else
                        isOn = true;

                }
                break;
            case ToggleType.Effect:
                {
                    if (SoundManager.Instance.IsEffectMute == true)
                        isOn = false;
                    else
                        isOn = true;

                }
                break;
            case ToggleType.JoyStick:
                {
                    if (JoyStick.Instance.NowStickType==StickType.UnFixed)
                        isOn = false;
                    else
                        isOn = true;
                }
                break;
        }

        SetToggleImage();
    }

    public void ClickAction()
    {
        if (isOn == true)
        {
            isOn = false;
        }
        else if (isOn == false)
        {
            isOn = true;
        }
        SetToggleImage();

        if (myBoolEvent != null)
            myBoolEvent.Invoke(isOn);
    }

    private void SetToggleImage()
    {
        if (toggleImage == null) return;

        if (isOn == true)
        {
            if (onImage != null)
                toggleImage.sprite = onImage;
        }
        else if (isOn == false)
        {
            if (offImage != null)
                toggleImage.sprite = offImage;
        }
    }
}
