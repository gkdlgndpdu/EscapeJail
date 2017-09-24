using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
  
    [HideInInspector]
    public bool nowRotate = false;
    private int count = 0;
    public void RotateWeapon()
    {
        if (nowRotate == true) return;

        float weaponAngle = this.transform.rotation.eulerAngles.z;
            

        if ((weaponAngle >= 0f && weaponAngle <= 90) ||
      weaponAngle >= 270f && weaponAngle <= 360)
        {
            this.transform.rotation = Quaternion.Euler(0f, 0f, -70f);

            nowRotate = true;
            iTween.RotateBy(this.gameObject, iTween.Hash
                  (
                  "z", .35,
                  "easeType", "easeInOutBack",
                  "onComplete", "NearWeaponRotateEnd",
                  "loopType", "pingPong",
                  "Time", 0.2f));
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(0f, 0f, -110f);

            nowRotate = true;
            iTween.RotateBy(this.gameObject, iTween.Hash
                  (
                  "z", -.35,
                  "easeType", "easeInOutBack",
                  "onComplete", "NearWeaponRotateEnd",
                  "loopType", "pingPong",
                  "Time", 0.2f));
        }

          
 

    }

    void NearWeaponRotateEnd()
    {
        count++;
        if (count >= 1)
        {
            count = 0;
        nowRotate = false;
        iTween.Stop(this.gameObject);
        }
        
    }
  

}
