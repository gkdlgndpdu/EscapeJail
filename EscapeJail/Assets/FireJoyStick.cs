using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class FireJoyStick : MonoBehaviour
{
    public static FireJoyStick Instance;
    //
    public Image stickImage;
    public Image backImage;
    // public Image middleImage;

    private Vector3 originPos = Vector3.zero;
    //초기 위치
    private Vector3 saveOriginPos = Vector3.zero;

    private Vector3 firstPos = Vector3.zero;
    private float stickRadius = 0.0f;

    private int debugvalue = 0;
    //도출된 이동방향
    private Vector3 fireDir;
    public Vector3 FireDir
    {
        get
        {
            return fireDir.normalized;
        }
    }

    //현재 터치 변수
    Touch touch;
    int fingerId = 999;
    private bool nowTouching = false;

  

    //



    private void Awake()
    {
        Instance = this;

        if (GameOption.FireStyle != FireStyle.Manual)
            this.gameObject.SetActive(false);
    }
    // Use this for initialization
    void Start()
    {
        originPos = stickImage.transform.position;
        saveOriginPos = originPos;
        firstPos = originPos;

        if (backImage != null)
            stickRadius = backImage.rectTransform.sizeDelta.x * 2f;

 

    }


    public void PotentialDrag()
    {
        if (nowTouching == true) return;
        //조이스틱 누른 손가락 찾음
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.touches[i].position.x > Screen.width * 0.5f)
            {
                touch = Input.GetTouch(i);
                fingerId = touch.fingerId;
                nowTouching = true;

                break;
            }
        }

        Drag();

    }

    public void Drag()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            //조이스틱을 터치중인 손가락일 때만.
            if (Input.touches[i].fingerId == fingerId)
            {
                touch = Input.GetTouch(i);
                originPos = saveOriginPos;

                //이동방향 계산
                fireDir = new Vector3(touch.position.x, touch.position.y, originPos.z) - originPos;
                fireDir.Normalize();
                //조이스틱 이동 반경 계산
                float touchAreaRadius = Vector3.Distance(originPos, new Vector3(touch.position.x, touch.position.y, originPos.z));


                if (touchAreaRadius > stickRadius)
                {
                    stickImage.rectTransform.position = originPos + (fireDir * stickRadius);
                  
                }              
                else
                {
                    stickImage.rectTransform.position = touch.position;
                }

                return;
            }
        }

    }
    private void Update()
    {

        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).position.x > Screen.width * 0.5f)
            {
                return;
            }
        }
        //오른쪽영역에 터치중인것이 하나도 없다.
        ResetStick();

    }


    //손가락 놨을때
    public void EndDrag()
    {

        for (int i = 0; i < Input.touchCount; i++)
        {
            //조이스틱을 터치중인 손가락일 때만.
            if (Input.touches[i].fingerId == fingerId)
            {
                ResetStick();
                break;
            }
        }

    }

    public void ResetStick()
    {
        //스틱 위치 원상복귀
        backImage.rectTransform.position = firstPos;
        stickImage.transform.position = firstPos;
        originPos = firstPos;
        fireDir = Vector3.zero;
        fingerId = 999;
        debugvalue = Input.touchCount;
        nowTouching = false;
    }

}
