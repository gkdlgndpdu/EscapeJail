using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
