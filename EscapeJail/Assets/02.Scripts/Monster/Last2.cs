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
        if (lineRenderer != null)
        {
            if (lineRenderer.enabled == true)
            {
                lineRenderer.SetPosition(0, this.transform.position);
                lineRenderer.SetPosition(1, target.position);
            }

        }
    }

    protected override void SetDie()
    {
        base.SetDie();
        AimOnOff(false);
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
        AimOnOff(true);
        while (true)
        {
            SetAnimation(MonsterState.Attack);
            yield return new WaitForSeconds(2f);
            AimOnOff(true);
            yield return new WaitForSeconds(2f);
        }
    }


    //에니메이션 이벤트로 호출
    public void FireGun()
    {
        FireWeapon();
        AimOnOff(false);

    }
}
