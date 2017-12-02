using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoodsUi : MonoBehaviour
{
    [SerializeField]
    private Text coinText;

    [SerializeField]
    private Text medalText;


    public void SetCoin(int value)
    {
        if (coinText == null) return;

        coinText.text = value.ToString();

    }
    public void SetMedal(int value)
    {
        if (medalText == null) return;

        medalText.text = value.ToString();

    }

}
