using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

[RequireComponent(typeof(LineRenderer))]
public class Last2 : MonsterBase
{
    private LineRenderer lineRenderer;

    private new void Awake()
    {
        base.Awake();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    private void UpdateLine()
    {
        if (lineRenderer != null&&target!=null)
        {
            if (lineRenderer.enabled == true)
            {
                lineRenderer.SetPosition(0, this.transform.position);
                int layerMask = (1 << LayerMask.NameToLayer("Player") | (1 << LayerMask.NameToLayer("Tile")) | (1 << LayerMask.NameToLayer("ItemTable")));

                Ray2D ray = new Ray2D(this.transform.position, target.position-this.transform.position);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f, layerMask);
                if (hit.collider != null)
                {
                    lineRenderer.SetPosition(1, hit.point);
                }
                else
                {
                    lineRenderer.SetPosition(1, this.transform.position);
                }

        
            }

        }
    }

    protected override void SetDie()
    {
        AimOnOff(false);
        base.SetDie();
    }

    private void AimOnOff(bool OnOff)
    {
        if (lineRenderer != null)
            lineRenderer.enabled = OnOff;
    }



    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Last2;
        hasBullet = true;
        //여기서는 여기 범위 내에 있으면 뒤로 도망감
        nearestAcessDistance = Random.Range(5f, 10f);

    }
    public override void ResetMonster()
    {
        base.ResetMonster();
    

        //여기서는 여기 범위 내에 있으면 뒤로 도망감
        nearestAcessDistance = Random.Range(3f, 8f);
     

    }

    protected override void StartMyCoroutine()
    {
        AimOnOff(false);
        StartCoroutine(FireRoutine());
    }

    protected override void SetWeapon()
    {
        nowWeapon.ChangeWeapon(new Last2Sniper());
    }






    // Update is called once per frame
    private void Update()
    {
        UpdateLine();

        RotateWeapon();
        if (canMove() == false) return;
        MoveAgainstTarget();

    }

    protected override IEnumerator FireRoutine()
    { 
        while (true)
        {
            AimOnOff(true);
           float delay = Random.Range(1.5f, 2.5f);
            yield return new WaitForSeconds(delay);
            SetAnimation(MonsterState.Attack);
            yield return new WaitForSeconds(2f);
            AimOnOff(false);
        }
    }


    //에니메이션 이벤트로 호출
    public void FireGun()
    {
        FireWeapon();
        AimOnOff(false);

    }
}
