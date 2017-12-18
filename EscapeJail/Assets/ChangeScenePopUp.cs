using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScenePopUp : MonoBehaviour
{

    private void OnEnable()
    {
        TimeManager.Instance.StopTime();
    }

    public void OkButtonClick()
    {
        TimeManager.Instance.ResumeTime();
        this.gameObject.SetActive(false);    
        GameManager.Instance.ChangeStage();
        

    }
    public void WaitButtonClickI()
    {
        TimeManager.Instance.ResumeTime();
        this.gameObject.SetActive(false);
    }
}
