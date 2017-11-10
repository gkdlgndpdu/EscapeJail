using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UI_QuickSlot : MonoBehaviour
{
    private Action quickSlotClick;

    public void ResetQuickSlot()
    {
        quickSlotClick = null;
    }

    public void SetQuickSlot(Action func)
    {
        quickSlotClick = func;
    }

    public void ClickButton()
    {
        if (quickSlotClick != null)
            quickSlotClick();
    }

	
}
