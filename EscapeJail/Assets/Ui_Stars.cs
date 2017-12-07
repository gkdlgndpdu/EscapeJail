using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_Stars : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> stars;


    public void SetStar(int count)
    {
        if (stars == null) return;
        for (int i = 0; i<stars.Count;i++)
        {
            if (i < count)
                stars[i].SetActive(true);
            else
                stars[i].SetActive(false);
        }
        
    }
   


}
