using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillButtonProgress : MonoBehaviour
{
    [SerializeField]
    private Image foreImage;
    [SerializeField]
    private Text label;

    private void Awake()
    {
        if (label != null)
            label.gameObject.SetActive(false);
    }

    public void SetProgress(float min, float max)
    {
        if (foreImage == null) return;
        foreImage.fillAmount = min / max;
    }

    public void SetText(string text)
    {
        if (label == null) return;
        if (label.gameObject.activeSelf == false)
            label.gameObject.SetActive(true);
        label.text = text;
    }
}
