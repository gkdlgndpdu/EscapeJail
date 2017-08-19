using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class WeaponBase : MonoBehaviour
{
    //컴포넌트
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Weapon nowWeapon;

    //임시UI
    [SerializeField]
    private bool UiOn = false;
    private void OnGUI()
    {
        if (UiOn == false) return;
        if (nowWeapon != null)
            GUI.Label(new Rect(0, 0, 500, 500), string.Format("{0}/{1}", nowWeapon.nowAmmo, nowWeapon.maxAmmo));
        // GUI.Label(new Rect(0, 0, 500, 500), "??????????????????????????");
    }

    private void Awake()
    {        
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }
    public void ChangeWeapon()
    {

    }

    public void FlipWeapon(bool value)
    {
        if (spriteRenderer != null)
            spriteRenderer.flipY = value;
    }



    public void SetWeapon(Weapon weapon)
    {
        nowWeapon = weapon;

        if (animator != null)
        {
            animator.runtimeAnimatorController = Resources.Load(string.Format("Animators/Weapon/{0}", weapon.weaponName)) as RuntimeAnimatorController;
            if (nowWeapon != null)
                nowWeapon.Initialize(animator);
        }

    }

    public void FireBullet(Vector3 firePos)
    {
        if (nowWeapon == null) return;

        nowWeapon.FireBullet(firePos);
  
        //if (animator != null&& nowWeapon.canFire()==true)
        //    animator.SetTrigger("FireTrigger");

    }

    private void OnDestroy()
    {
        nowWeapon = null;
    }

    private void Update()
    {
        if (nowWeapon != null)
            nowWeapon.WeaponUpdate();
    }
}
