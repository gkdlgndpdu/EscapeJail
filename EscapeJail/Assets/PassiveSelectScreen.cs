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

    private RectTransform rectTr;

    private List<PassiveSlot_Ui> slotList;

    private PassiveSlot_Ui nowSelectSlot =null;

    private void Awake()
    {
        slotList = new List<PassiveSlot_Ui>();
        rectTr = slotsParent.GetComponent<RectTransform>();
        MakeSlots();
        UpdateMedalText();
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


    private void MakeSlots()
    {
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

        int medal = PlayerPrefs.GetInt(GoodsType.Medal.ToString(), 0);
        medalText.text = medal.ToString();
    }
}
