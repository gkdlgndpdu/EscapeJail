using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;
using UnityEngine.Events;
public class PlayerButtonLinker : MonoBehaviour
{
    CharacterBase playerData;

    [SerializeField]
    private EventTrigger getButton;
    [SerializeField]
    private EventTrigger swapButton;
    [SerializeField]
    private EventTrigger SkillButton;
    [SerializeField]
    private StayButton ShotButton;

  
    

    private void Start()
    {
        playerData = GamePlayerManager.Instance.player;

        if (playerData == null) return;

        LinkShotButton();
        LinkGetButton();
        LinkSwapButton();
        LinkSkillButton();
    }

    private void LinkShotButton()
    {
        if (ShotButton == null) return;
        ShotButton.myEvent.AddListener(playerData.FireWeapon);
    }

    private void LinkGetButton()
    {
        if (getButton == null) return;
        getButton.triggers.Add(MakeTriggerEntry(playerData.ReactiveButtonClick));
    }
    private void LinkSwapButton()
    {
        if (swapButton == null) return;       
        swapButton.triggers.Add(MakeTriggerEntry(playerData.ChangeWeapon));
    }

    private void LinkSkillButton()
    {
        if (SkillButton == null) return;
        SkillButton.triggers.Add(MakeTriggerEntry(playerData.UseCharacterSkill));
    }

    private EventTrigger.Entry MakeTriggerEntry(System.Action func)
    {    
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => { func(); });
 


        return entry;
    }

}
