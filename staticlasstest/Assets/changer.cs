using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class changer : MonoBehaviour
{
    public static changer Instance;

    private void Awake()
    {
        if(Instance==null)
        Instance = this;
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.PageUp))
        {

            SceneManager.LoadSceneAsync(0);
        }

        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            SceneManager.LoadSceneAsync(1);
        }
    }
}
