using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BackGroundTile : MonoBehaviour
{
    public static BackGroundTile Instance;
    private MeshRenderer meshRenderer;

    [SerializeField]
    private Texture stage1;
    [SerializeField]
    private Texture stage2;
    [SerializeField]
    private Texture stage3;
    [SerializeField]
    private Texture stage4;
    [SerializeField]
    private Texture stage5;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>(); 

        this.transform.position = Vector3.zero;

        Instance = this;

    }

    public void Initialize(Vector3 posit,Vector2 size,Vector2 tileNum)
    {

        if (meshRenderer != null)
        {     
            meshRenderer.material.SetTextureScale("_MainTex", tileNum);
            switch (StagerController.Instance.NowStageLevel)
            {
                case 1:
                    meshRenderer.material.mainTexture = stage1;
                    break;
                case 2:
                    meshRenderer.material.mainTexture = stage2;
                    break;
                case 3:
                    meshRenderer.material.mainTexture = stage3;
                    break;
                case 4:
                    meshRenderer.material.mainTexture = stage4;
                    break;
                case 5:
                    meshRenderer.material.mainTexture = stage5;
                    break;
            }           
        
        }
        this.transform.localScale = size;

        this.transform.position = posit;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

}
