using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuBackGround : MonoBehaviour
{
    [SerializeField]
    private Image redImage;


    IEnumerator RedImageFade()
    {
        if (redImage == null) yield break;
        while (true)
        {
            redImage.gameObject.SetActive(!redImage.gameObject.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }
	// Use this for initialization
	void Start ()
    {
        StartCoroutine(RedImageFade());
	}
	
	
}
