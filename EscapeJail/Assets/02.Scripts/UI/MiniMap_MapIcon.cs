using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class MiniMap_MapIcon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private BoxCollider2D boxCollider;
    [SerializeField]
    SpriteRenderer portalIcon;

    private MapModuleBase linkModule = null;
    public MapModuleBase LinkModule
    {
        get
        {
            return linkModule;
        }
    }

    private bool hasPortal = false;
    public bool HasProtal
    {
        get
        {
            return hasPortal;
        }
    }
    private bool canUsePortal = false;
    public bool CanUsePortal
    {
        get
        {
            return canUsePortal;
        }
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();


        if (spriteRenderer != null)
            spriteRenderer.color = Color.red;
    }

    public void SetPortal(bool hasPortal, MapModuleBase module)
    {
        if (portalIcon == null) return;

        if (hasPortal == true)
        {
            portalIcon.gameObject.SetActive(true);
            portalIcon.color = Color.red;
        }
        else if (hasPortal == false)
        {
            portalIcon.gameObject.SetActive(false);
        }
        this.hasPortal = hasPortal;
        this.linkModule = module;
    }

    public void SetClear()
    {
        if (spriteRenderer != null)
            spriteRenderer.color = Color.white;

        canUsePortal = true;

        //이미지 원색으로 변경
        if(portalIcon.gameObject.activeSelf==true)
        portalIcon.color = Color.white;
    }

}
