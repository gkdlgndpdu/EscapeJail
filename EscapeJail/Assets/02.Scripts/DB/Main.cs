using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;
public class Main : MonoBehaviour {
	private string description;
    private StringBuilder builder;
    public Text text;
	// Use this for initialization
	void Start ()
    {		
		dbAccess db = GetComponent<dbAccess>();
		
		db.OpenDB("TestDB.db");

        builder =db.SingleSelectWhere();
        text.text = builder.ToString();


        db.CloseDB();
	}

	// Update is called once per frame
	void Update () {
		
			
	}
	

}
