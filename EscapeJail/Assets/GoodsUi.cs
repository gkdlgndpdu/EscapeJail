using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoodsUi : MonoBehaviour
{
    [SerializeField]
    private Text text;


    public void SetText(int value)
    {
        if (text == null) return;

        text.text = value.ToString();

    }

}
