using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;

public class GoogleService : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        PlayGamesPlatform.Instance.Authenticate((bool success) =>
            {
                if (success == true)
                {
                    //로그인 성공처리
                }
                else
                {
                    //로그인 실패처리
                }
            });
   





    }

    public void SignOut()
    {
        PlayGamesPlatform.Instance.SignOut();
    }


}
