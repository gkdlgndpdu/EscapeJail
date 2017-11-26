using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterSelector : MonoBehaviour
{
    [SerializeField]
    private GameObject characterSlotPrefab;
 



    private void Start()
    {
        MakeCharacterSlots();
    }

    private void MakeCharacterSlots()
    {
        if (characterSlotPrefab == null) return;

        for(int i = 0; i < (int)CharacterType.CharacterEnd; i++)
        {
            GameObject makeObj = GameObject.Instantiate(characterSlotPrefab,this.transform);
            if (makeObj != null)
            {
                CharacterSlot_Ui slot = makeObj.GetComponent<CharacterSlot_Ui>();
                if (slot != null)
                {
                    slot.Initialize((CharacterType)i);
                }
            }
        }
    }

    private void ChangeScene()
    {    
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int)SceneName.GameScene);

    }
}
