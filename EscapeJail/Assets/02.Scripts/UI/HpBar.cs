using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
public class HpBar : MonoBehaviour
{
    [SerializeField]
    private Image foreGround;
    [SerializeField]
    private Text text;
    [SerializeField]
    private Image ArmorBar;

    private StringBuilder stringBuilder;

    private void Awake()
    {
        stringBuilder = new StringBuilder();
    }

    public void SetArmorBar(float ratio)
    {
        if (ArmorBar != null)
        {
            ArmorBar.fillAmount = ratio;
        }
    }

    public void SetHpBar(float min, float max)
    {

        if (foreGround != null)
        {
            foreGround.fillAmount = min / max;
        }

        if (stringBuilder != null&& text!=null)
        {
            stringBuilder.Length = 0;
            stringBuilder.AppendFormat("{0}/{1}", min, max);
            text.text = stringBuilder.ToString();


        }


    }
}
