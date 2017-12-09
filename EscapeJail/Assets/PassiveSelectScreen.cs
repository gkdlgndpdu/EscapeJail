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

    private PassiveSlot_Ui nowSelectSlot =null;

    private PassiveType nowBuyingPassive;

   

    private void Awake()
    {
        slotList = new List<PassiveSlot_Ui>();
        rectTr = slotsParent.GetComponent<RectTransform>();
        MakeSlots();
        UpdateMedalText();
    }

    public void OpenPassiveBuyScreen(PassiveType type,PassiveDB data)
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

    public void ClosePassiveBuyScreen()
    {
        //갱신
        RenewPassiveSlot();
        if (passiveBuyScreen == null) return;
        passiveBuyScreen.gameObject.SetActive(false);
     
    }
    public void BuyButtonClick()
    {
        DatabaseLoader.Instance.BuyPassiveItem(this.nowBuyingPassive);
        if (playerGoods != null)
        {
            PassiveDB data = DatabaseLoader.Instance.passiveDB[nowBuyingPassive];
            playerGoods.UseMedals(data.price);
            UpdateMedalText();

        }
        ClosePassiveBuyScreen();
    }

    public void RegistSelectSlot(PassiveSlot_Ui slot)
    {
        //첫번째 선택
        if (nowSelectSlot == null)
        {       
            nowSelectSlot = slot;
            slot.SetSelect(true);
        }
        else if(nowSelectSlot != null)
        {
            //같은애 또들어옴
            if (nowSelectSlot == slot)
            {
                nowSelectSlot.SetSelect(false);
                nowSelectSlot = null;
                return;
            }
            //방금 들어온애 선택
            slot.SetSelect(true);
            //이전애 선택 하제
            nowSelectSlot.SetSelect(false);
            //방금 들어온애를 현재 선택 슬롯으로
            nowSelectSlot = slot;

        }
    }

    private void RenewPassiveSlot()
    {
        if (slotList == null) return;

        for(int i=0;i< slotList.Count; i++)
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

        int makeSlotNum = passiveDB.Count ;
        float eachDistance = grid.cellSize.y + grid.spacing.y;

        foreach (KeyValuePair<PassiveType, PassiveDB> data in passiveDB)
        {
            PassiveSlot_Ui slot = Instantiate(slotPrefab, slotsParent);
            slot.Initialize(data.Key, data.Value,this);
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


 


}
