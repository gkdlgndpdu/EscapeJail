using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ExplosionEffect : MonoBehaviour
{
    
    private Animator animator;
    /// <summary>
    /// effectName인자에 null을 넣으면 기본이펙트가 나옴
    /// </summary>
    /// <param name="effectName"></param>
    /// <param name="lifeTime"></param>
    public void Initilaize(Vector3 startPos,string effectName,float lifeTime=1)
    {
        this.gameObject.transform.position = startPos;

        if (effectName != null)
        animator.runtimeAnimatorController = Resources.Load(string.Format("Animators/Weapon/{0}", effectName)) as RuntimeAnimatorController;
       
        Invoke("EffectOff", lifeTime);        
    }

    void EffectOff()
    {
        this.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
