using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class MessageBar : MonoBehaviour
{
    [SerializeField]
    private Text messageText;

    private Image image;

    [SerializeField]
    private Transform showPosit;

    [SerializeField]
    private Transform hidePosit;

    public static MessageBar Instance;

    private float hideDelay = 2f;

    float moveSpeed = 1f;

    private void Awake()
    {
        Instance = this;
        image = GetComponent<Image>();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Start()
    {
        ResetInfoBar();
    }

    public void ShowInfoBar(string text,Color color)
    {
        if (text == null) return;
  
        if (image != null)
            image.color = color;    
        messageText.text = text;

      
        iTween.MoveTo(this.gameObject, showPosit.position, moveSpeed);

        StopCoroutine("AutoHideRoutine");
        StartCoroutine("AutoHideRoutine");
    }

    private IEnumerator AutoHideRoutine()
    {
        yield return new WaitForSeconds(hideDelay);
        ResetInfoBar();
    }

    public void ResetInfoBar()
    {
        iTween.MoveTo(this.gameObject, hidePosit.position, moveSpeed);
    }
}
