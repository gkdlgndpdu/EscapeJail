using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{


	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //메달 생성
            DropGoods medal = ObjectManager.Instance.coinPool.GetItem();
            medal.Initiatlize(this.transform.position, 10, GoodsType.Medal);
        }	
	}
}
