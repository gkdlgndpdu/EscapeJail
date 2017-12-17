using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField]
    private GameObject characterSlotPrefab;

    [SerializeField]
    private GridLayoutGroup grid;

    [SerializeField]
    private GameObject passiveUiParent;

    private CharacterSlot_Ui nowSelectSlot;

    private RectTransform rectTr;

    [SerializeField]
    private Text characterInfoText;

    [SerializeField]
    private GameObject selectButton;

    public void RegistSelectSlot(CharacterSlot_Ui slot)
    {
        this.nowSelectSlot = slot;
        UpdateCharacterInfoText();
        SelectButtonOnOff(true);
    }
    public void ShowHowToGet(CharacterSlot_Ui slot)
    {
        this.nowSelectSlot = slot;
        UpdataCharacterHowToGet();
        SelectButtonOnOff(false);


    }
    private void SelectButtonOnOff(bool OnOff)
    {
        if (selectButton == null) return;
        selectButton.SetActive(OnOff);
    }
    private void UpdateCharacterInfoText()
    {
        if (nowSelectSlot == null) return;
        if (characterInfoText == null) return;
        CharacterDB db = DatabaseLoader.Instance.GetCharacterDB(nowSelectSlot.CharacterType);
        if (db == null) return;
        characterInfoText.text = string.Format("{0} \nSkill : {1} \n {2}", nowSelectSlot.CharacterType.ToString(), db.skillName, db.description);
    }

    private void UpdataCharacterHowToGet()
    {
        if (characterInfoText == null) return;
        CharacterDB db = DatabaseLoader.Instance.GetCharacterDB(nowSelectSlot.CharacterType);
        if (db == null) return;
        characterInfoText.text = string.Format("{0}\nHow to get \n{1}", nowSelectSlot.CharacterType.ToString(), db.howToGet);

    }

    private void Awake()
    {
        rectTr = grid.GetComponent<RectTransform>();
    }

    private void Start()
    {
        MakeCharacterSlots();
        SelectButtonOnOff(false);
    }

    private void MakeCharacterSlots()
    {
        if (characterSlotPrefab == null) return;

        for(int i = 0; i < (int)CharacterType.CharacterEnd; i++)
        {
            GameObject makeObj = GameObject.Instantiate(characterSlotPrefab,this.transform);
            if (makeObj != null)
            {
                CharacterSlot_Ui slot = makeObj.GetComponent<CharacterSlot_Ui>();
                if (slot != null)
                {
                    slot.Initialize((CharacterType)i, PassiveUiOnOff,this);
                }
            }
        }

        float eachDistance = grid.cellSize.y + grid.spacing.y;
        if (rectTr != null)
        {
            rectTr.sizeDelta = new Vector2(eachDistance * ((int)CharacterType.CharacterEnd), 100f);
        }

    }
  

    public void PassiveUiOnOff()
    {
        if (passiveUiParent == null) return;
        passiveUiParent.gameObject.SetActive(!passiveUiParent.gameObject.activeSelf);
    }
    public void GameStart()
    {
        SceneManager.Instance.ChangeScene(SceneName.StoryScene);
    }
}
