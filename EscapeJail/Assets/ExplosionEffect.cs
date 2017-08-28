using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ExplosionEffect : MonoBehaviour
{

    private Animator animator;
    private bool isEffectOff = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// effectName인자에 null을 넣으면 기본이펙트가 나옴
    /// </summary>
    /// <param name="effectName"></param>
    /// <param name="lifeTime"></param>
    public void Initilaize(Vector3 startPos, string effectName, float lifeTime = 1,int size =1)
    {
        isEffectOff = false;

        this.gameObject.transform.position = startPos;

        this.transform.localScale = Vector3.one * size;

        if (effectName != null && animator != null)
        {
            Object obj = ObjectManager.LoadGameObject(string.Format("Animators/Effect/{0}", effectName));
              
            if (obj != null)
            {
                RuntimeAnimatorController effectAnim = obj as RuntimeAnimatorController;
                if (effectAnim != null)
                {
                    animator.runtimeAnimatorController = effectAnim;

                }
            }
        }


       
    }

    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }


    private void LateUpdate()
    {
        if (animator == null) return;

        if (AnimatorIsPlaying() == false&& isEffectOff==false)
        {
            Invoke("EffectOff", 0.5f);
            isEffectOff = true;

        }

    }

    void EffectOff()
    {
        this.gameObject.SetActive(false);
    }


}
