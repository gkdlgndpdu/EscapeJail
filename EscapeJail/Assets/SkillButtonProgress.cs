using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillButtonProgress : MonoBehaviour
{
    [SerializeField]
    private Image foreImage;

	public void SetProgress(float min, float max)
    {
        if (foreImage == null) return;
        foreImage.fillAmount = min / max;

    }
}
