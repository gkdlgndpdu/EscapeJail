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

    private RectTransform rectTr;
    

    private void Awake()
    {
        rectTr = slotsParent.GetComponent<RectTransform>();
        MakeSlots();
        
    }
     

    private void MakeSlots()
    {
        int makeSlotNum =77;
        for(int i = 0; i < makeSlotNum; i++)
        {
            Instantiate(slotPrefab, slotsParent);
        }

        if (invisibleTouchArea != null)
        {
            invisibleTouchArea.transform.localScale = new Vector3(5f, makeSlotNum + 6f, 1f);
        }

        if (rectTr != null)
        {
            rectTr.sizeDelta = new Vector2(500f, (grid.cellSize.y+grid.spacing.y)*((float)(makeSlotNum-4)));
        }
    }
}
