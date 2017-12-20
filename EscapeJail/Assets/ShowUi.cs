using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowUi : MonoBehaviour
{
    [SerializeField]
    private Button buyButton;
    [SerializeField]
    private Text buyText;

    public void BuyTrader()
    {
        UnityIAPManager.Instance.BuyTrader();
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        CheckBuy();
    }

    private void OnDisable()
    {
        CheckBuy();
    }

    private void CheckBuy()
    {
        if (buyButton == null || buyText == null) return;

        CharacterDB data = DatabaseLoader.Instance.GetCharacterDB(CharacterType.Trader);
        if (data != null)
        {
            if (data.hasCharacter == true)
            {
                buyButton.enabled = false;
                buyText.text = "Sold out";
                buyText.color = Color.red;
            }
            else
            {
                buyButton.enabled = true;
            }
        }
    }

}
