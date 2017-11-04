using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ScientistBoss
{


    public enum ChildType
    {
        _1,
        _2,
        _3,
        _4
    }

    [RequireComponent(typeof(BossEventQueue))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class ChildComputer : CharacterInfo
    {
        private Animator animator;
        private BoxCollider2D boxCollider;
        public ChildType childType;
        [HideInInspector]
        public bool isDead = false;
        private ScientistBoss parentComputer;
        private Image hudImage;

        public List<Transform> computer1FirePos;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            boxCollider = GetComponent<BoxCollider2D>();
            hudImage = GetComponentInChildren<Image>();
        }

        public override void GetDamage(int damage)
        {
            if (isDead == true) return;

            hp -= damage;
            UpdateHud();
            if (hp <= 0)
            {
                SetDie();
            }
        }

        private void SetDie()
        {
            isDead = true;
            Action(Actions.Dead);
            RemoveInMonsterList();
            parentComputer.ChildDead();
        }

        protected void UpdateHud()
        {
            if (hudImage != null)
                hudImage.fillAmount = (float)hp / (float)hpMax;

        }

        public void StartPattern(ScientistBoss parent,int hp =40)
        {
            parentComputer = parent;
            AddToMonsterList();
            SetHp(hp);
        }

        private void AddToMonsterList()
        {
            MonsterManager.Instance.AddToList(this.gameObject);
        }

        private void RemoveInMonsterList()
        {
            MonsterManager.Instance.DeleteInList(this.gameObject);
        }

        public void Action(Actions action)
        {
            switch (action)
            {
                case Actions.FireStart:
                    {
                        if (animator != null)
                            animator.SetTrigger("FireTrigger");
                    }
                    break;
                case Actions.FireEnd:
                    {
                        if (animator != null)
                            animator.SetTrigger("FireEndTrigger");
                    }
                    break;
                case Actions.HideStart:
                    {                   
                        if (animator != null)
                            animator.SetTrigger("HideTrigger");

                    }
                    break;
                case Actions.HideEnd:
                    {              
                        if (animator != null)
                            animator.SetTrigger("HideEndTrigger");
                    }
                    break;
                case Actions.Dead:
                    {
                        if (animator != null)
                            animator.SetTrigger("DeadTrigger");
                    }
                    break;
            }
        }

        public void FireEnd()
        {
            Action(Actions.FireEnd);
        }

        public void FireStart()
        {
            if (isDead == true) return;
            Action(Actions.FireStart);
        }
        public void ColliderOnOff(bool OnOff)
        {
            if (boxCollider != null)
                boxCollider.enabled = OnOff;
        }


        public void HideOnOff(bool OnOff)
        {
            if (isDead == true) return;

            if (OnOff == true)
            {
                Action(Actions.HideStart);
                ColliderOnOff(false);
                RemoveInMonsterList();
            }
            else
            {
                Action(Actions.HideEnd);
                ColliderOnOff(true);
                AddToMonsterList();
            }
        }

   

        public void FireWeapon()
        {
            switch (childType)
            {
                case ChildType._1:
                    {
                        if (computer1FirePos == null) return;

                        Vector3 PlayerPos = GamePlayerManager.Instance.player.transform.position;
                        Vector3 fireDIr = PlayerPos - this.transform.position;
                        float reboundValue = 30f;

                        for (int i=0;i< computer1FirePos.Count; i++)
                        {
                            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                            if (bullet != null)
                            {
                                bullet.gameObject.SetActive(true);
                                Vector3 fireDirection = Quaternion.Euler(0f, 0f, Random.Range(-reboundValue, reboundValue)) * fireDIr;
                                bullet.Initialize(computer1FirePos[i].position, fireDirection.normalized, 5f, BulletType.EnemyBullet);
                                bullet.InitializeImage("white", false);
                                bullet.SetEffectName("revolver");
                            }
                        }
                      
                    }
                    break;
                case ChildType._2:
                    {
                        Vector3 PlayerPos = GamePlayerManager.Instance.player.transform.position;
                        Vector3 fireDIr = PlayerPos - this.transform.position;
                        Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                        if (bullet != null)
                        {                           
                            bullet.gameObject.SetActive(true);
                            bullet.Initialize(this.transform.position, fireDIr.normalized, 1f, BulletType.EnemyBullet);
                            bullet.InitializeImage("white", false);
                            bullet.SetEffectName("revolver");
                        }                    
                      

                      

                    }
                    break;
                case ChildType._3:
                    {

                        Vector3 PlayerPos = GamePlayerManager.Instance.player.transform.position;
                        Vector3 fireDIr = PlayerPos - this.transform.position;
                        for (int k = 0; k < 2; k++)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                                if (bullet != null)
                                {
                                    bullet.gameObject.SetActive(true);
                             
                                    Vector3 realFireDir = Quaternion.Euler(0f, 0f, -5f + 10f * k) * fireDIr;
                                  
                                    bullet.Initialize(this.transform.position, realFireDir.normalized, 8f - (float)i, BulletType.EnemyBullet);
                                    bullet.InitializeImage("white", false);
                                    bullet.SetEffectName("revolver");
                                }
                            }
                        }

              
                 
                    }
                    break;
                case ChildType._4:
                    {                       

                        float bulletSpeed = 8f;

                        Vector3 PlayerPos = GamePlayerManager.Instance.player.transform.position;
                        Vector3 fireDIr = PlayerPos - this.transform.position;                   

                        for(int i = 0; i < 18; i++)
                        {
                            Vector3 fd = Quaternion.Euler(0f, 0f, i*20f)* fireDIr;              
                            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                            if (bullet != null)
                            {
                                bullet.gameObject.SetActive(true);                        
                                bullet.Initialize(this.transform.position+fd.normalized*1f, fireDIr.normalized, bulletSpeed, BulletType.EnemyBullet);
                                bullet.InitializeImage("white", false);
                                bullet.SetEffectName("revolver");
                                bullet.SetDestroyByCollision(false,false);
                            }
                        }

                       
                    }
                    break;
            }


        }

   

    }






}