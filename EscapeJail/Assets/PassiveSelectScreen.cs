using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveSelectScreen : MonoBehaviour
{
    [SerializeField]
    private Transform slotsParent;
    [SerializeField]
    private Image invisibleTouchArea;
    [SerializeField]
    private PassiveSlot_Ui slotPrefab;
    [SerializeField]
    private GridLayoutGroup grid;
    [SerializeField]
    private Text medalText;

    [SerializeField]
    private PlayerGoods playerGoods;

    [SerializeField]
    private PassiveBuyScreen passiveBuyScreen;

    private RectTransform rectTr;

    private List<PassiveSlot_Ui> slotList;

    private List<PassiveSlot_Ui> nowSelectSlotList = new List<PassiveSlot_Ui>();

    private PassiveType nowBuyingPassive;

    [SerializeField]
    private List<Image> nowSelectPassiveIcons;


    private void Awake()
    {
        slotList = new List<PassiveSlot_Ui>();
        rectTr = slotsParent.GetComponent<RectTransform>();
        MakeSlots();
        UpdateMedalText();

        UpdateNowSelectPassiveUi();
    }

    public void OpenPassiveBuyScreen(PassiveType type, PassiveDB data)
    {
        if (passiveBuyScreen == null) return;
        //돈이 되면 창 열기
        if (playerGoods.GetCurrentMedal() >= data.price)
        {
            passiveBuyScreen.gameObject.SetActive(true);
            passiveBuyScreen.Initialize(type, data);
            nowBuyingPassive = type;
        }
        //돈이 부족할때
        else
        {
            Debug.Log("돈이 부족합니다");
        }
        //

    }

    public void RemoveInFirstSlot()
    {
        if (nowSelectSlotList == null) return;
        if (nowSelectSlotList.Count < 1) return;
        RemoveInList(nowSelectSlotList[0]);
    }
    public void RemoveInSecondSlot()
    {
        if (nowSelectSlotList == null) return;
        if (nowSelectSlotList.Count < 2) return;
        RemoveInList(nowSelectSlotList[1]);
    }

    public void ClosePassiveBuyScreen()
    {
        //갱신
        RenewPassiveSlot();
        if (passiveBuyScreen == null) return;
        passiveBuyScreen.gameObject.SetActive(false);

    }
    public void BuyButtonClick()
    {
        DatabaseLoader.Instance.BuyPassiveItem(nowBuyingPassive);
        if (playerGoods != null)
        {
            PassiveDB data = DatabaseLoader.Instance.passiveDB[nowBuyingPassive];
            playerGoods.UseMedals(data.price);
            UpdateMedalText();
        }
        //로컬 데이터 저장                             //0없음 1있음
        PlayerPrefs.SetInt(nowBuyingPassive.ToString(), 1);

        ClosePassiveBuyScreen();
    }

    private void AddToSelect(PassiveSlot_Ui slot)
    {
        if (nowSelectSlotList == null) return;
        slot.SetSelect(true);
        nowSelectSlotList.Add(slot);
        UpdateNowSelectPassiveUi();
    }
    private void RemoveInList(PassiveSlot_Ui slot)
    {
        if (nowSelectSlotList == null) return;
        slot.SetSelect(false);
        nowSelectSlotList.Remove(slot);
        UpdateNowSelectPassiveUi();
    }
    private bool HasSelectSlot(PassiveSlot_Ui slot)
    {
        if (nowSelectSlotList == null) return false;
        return nowSelectSlotList.Contains(slot);
    }

    public void RegistSelectSlot(PassiveSlot_Ui slot)
    {
        if (nowSelectSlotList == null) return;
        if (HasSelectSlot(slot) == true)
        {
            RemoveInList(slot);
            return;
        }
        if (nowSelectSlotList.Count >= 2) return;

        AddToSelect(slot);


    }

    private void RenewPassiveSlot()
    {
        if (slotList == null) return;

        for (int i = 0; i < slotList.Count; i++)
        {
            PassiveDB data = DatabaseLoader.Instance.passiveDB[slotList[i].passiveType];
            slotList[i].Initialize(slotList[i].passiveType, data, this);
        }
    }


    private void MakeSlots()
    {
        if (DatabaseLoader.Instance == null) return;

        Dictionary<PassiveType, PassiveDB> passiveDB = DatabaseLoader.Instance.passiveDB;
        if (passiveDB == null) return;

        int makeSlotNum = passiveDB.Count;
        float eachDistance = grid.cellSize.y + grid.spacing.y;

        foreach (KeyValuePair<PassiveType, PassiveDB> data in passiveDB)
        {
            PassiveSlot_Ui slot = Instantiate(slotPrefab, slotsParent);
            slot.Initialize(data.Key, data.Value, this);
            slotList.Add(slot);
        }

        if (invisibleTouchArea != null)
        {
            invisibleTouchArea.transform.localScale = new Vector3(4.6f, ((float)makeSlotNum + 2.5f) * 1.1f, 1f);
        }

        if (rectTr != null)
        {
            rectTr.sizeDelta = new Vector2(500f, eachDistance * ((float)(makeSlotNum - 3)));
        }
    }

    private void UpdateMedalText()
    {
        if (medalText == null) return;
        if (playerGoods == null) return;

        int medal = playerGoods.GetCurrentMedal();
        medalText.text = medal.ToString();
    }

    //테스트용 코드
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {

            int prefMedal = PlayerPrefs.GetInt(GoodsType.Medal.ToString(), 0);
            prefMedal += 100;
            PlayerPrefs.SetInt(GoodsType.Medal.ToString(), prefMedal);
            UpdateMedalText();
        }
    }



    private void UpdateNowSelectPassiveUi()
    {
        if (nowSelectSlotList == null) return;
        if (nowSelectPassiveIcons == null) return;
        for(int i=0;i< nowSelectPassiveIcons.Count; i++)
        {
            if (i < nowSelectSlotList.Count)
            {
                nowSelectPassiveIcons[i].color = Color.white;
                //아이콘

                Sprite loadSprite = Resources.Load<Sprite>("Sprites/icon/Passive/" + nowSelectSlotList[i].passiveType.ToString());
                if (loadSprite != null)
                    nowSelectPassiveIcons[i].sprite = loadSprite;
            }
            else
            {
                nowSelectPassiveIcons[i].color = new Color(1f,1f,1f,0f);
                //아이콘


            }
        }
    }

}
