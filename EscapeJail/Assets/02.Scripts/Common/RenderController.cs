using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 플레이어,적들간의 그려주는 순서를 컨트롤 해줍니다
/// 컨트롤할 오브젝트의 spriteRender가 붙어있는 오브젝트에 RenderOrder스크립트를 
/// 붙여주면 알아서 작동합니다
/// </summary>
public class RenderController : MonoBehaviour
{
    public static RenderController Instance;
    private List<RenderOrder> renderObjects = new List<RenderOrder>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void AddToRenderList(RenderOrder renderObject)
    {
        if (renderObject != null)
        {
            renderObjects.Add(renderObject);
        }
    }

    public void RemoveInRenderList(RenderOrder renderObject)
    {
        if (renderObject != null)
        {
            renderObjects.Remove(renderObject);
        }
    }

    public void SortingRenderer()
    {
        if (renderObjects != null)
        {      
            renderObjects.Sort((a, b) =>
            {
                if (a.transform.position.y > b.transform.position.y)
                    return -1;
                else if (a.transform.position.y < b.transform.position.y)
                    return 1;
                else return 0;
            });
        }

        for (int i = 0; i < renderObjects.Count; i++)
        {
            int layer = GameConstants.PlayerLayerMin;
            renderObjects[i].SetOrder(layer+i);
        }
    }

    private void Update()
    {
        SortingRenderer();
    }


}
