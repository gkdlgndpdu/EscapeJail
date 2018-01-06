using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MindArrow_Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform target = null;
    private List<GameObject> monsterList;
    private int power = 1;
    private bool nowFreeMove = false;
    private float freeTime = 1f;
    private float moveSpeed = 5f;
    private float rotateSpeed = 20f;
    private float lifeTime = 9f;
    private bool firstFind = true;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private Vector3 moveDir;

    public void Initialize(Vector3 firePos, Vector3 fireDir, int power)
    {
        this.transform.position = firePos;
        this.power = power;

        monsterList = MonsterManager.Instance.MonsterList;

        moveDir = fireDir.normalized;

        FindNextTarget();

        StartCoroutine(AutoOffRoutine());
    }

    IEnumerator AutoOffRoutine()
    {
        yield return new WaitForSeconds(lifeTime);
        DetsroyArrow();
    }

    private void DetsroyArrow()
    {
        StopAllCoroutines();
        Destroy(this.gameObject);
    }

    private void FindNextTarget()
    {
        if (monsterList == null) return;
        if (monsterList.Count == 0)
        {        
            return;
        }

        monsterList.Sort((a, b) => { return Vector3.Distance(a.transform.position, this.transform.position).CompareTo(Vector3.Distance(b.transform.position, this.transform.position)); });
        firstFind = false;
        target = monsterList[0].transform;
    }


    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void moveRoutine()
    {
        if (rb == null) return;

        if (target != null)
        {
            if (target.gameObject.activeSelf == true)
            {
                Vector3 targetDir = target.transform.position - this.transform.position;
                moveDir = Vector3.Lerp(this.moveDir, targetDir, Time.deltaTime * rotateSpeed);
                moveDir.Normalize();
            }
        }

        rb.velocity = moveDir.normalized * moveSpeed;

        this.transform.rotation = Quaternion.Euler(0f, 0f, MyUtils.GetAngle(this.transform.position, this.transform.position + moveDir));

        if (monsterList != null)
        {
            if (monsterList.Count != 0 || firstFind ==true)
            {
                FindNextTarget();
            }
        }

    }

    private void Update()
    {
        moveRoutine();
    }
    private IEnumerator freeTimeRoutine()
    {
        nowFreeMove = true;
        target = null;
        yield return new WaitForSeconds(freeTime);
        nowFreeMove = false;
        FindNextTarget();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterInfo characterInfo = collision.gameObject.GetComponent<CharacterInfo>();
        if (characterInfo != null)
        {
            characterInfo.GetDamage(power);
            if (nowFreeMove == false)
                StartCoroutine(freeTimeRoutine());

        }
    }


}
