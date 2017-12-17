using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Portal : MonoBehaviour
{
    private Animator animator;
    private MapModuleBase parentModule;
    private bool isBossPortal =false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        if(animator!=null)
        animator.speed = 0f;
    }

    public void WhenBossDie()
    {
        this.transform.localScale = Vector3.zero;

        iTween.ScaleTo(this.gameObject, Vector3.one * 4f, 1f);
    }

    public void SetBossPortal()
    {
        isBossPortal = true;
    }

    public void Initialize(MapModuleBase parentModule)
    {
        this.parentModule = parentModule;
        this.transform.parent = parentModule.transform;
        this.transform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBossPortal != true) return;
        //스테이지 넘어갈지 팝업창 띄워주기
        if(collision.gameObject.CompareTag("Player")==true)
        BossIntroduceWindow.Instance.ChangeScenePopupOn();
;    }
}
