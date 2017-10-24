using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Mono.Data.SqliteClient;


public class TestData
{
    public string ID;
    public string Hp;
    public string MoveSpeed;
    public string AttackPower;



    public TestData(string Id, string hp, string movespeed, string attackpower)
    {
        ID = Id;
        Hp = hp;
        MoveSpeed = movespeed;
        AttackPower = attackpower;

    }
}

public class dbAccess : MonoBehaviour
{
    private string connection;
    private IDbConnection dbcon;
    private IDbCommand dbcmd;
    private IDataReader reader;
    private StringBuilder builder = new StringBuilder();


    public void OpenDB(string p)
    {
        Debug.Log("Call to OpenDB:" + p);
        // check if file exists in Application.persistentDataPath
        string filepath;
#if UNITY_EDITOR
        filepath = Application.streamingAssetsPath + "/" + p;
#else
  string filepath = Application.persistentDataPath + "/" + p;
#endif


        if (!File.Exists(filepath))
        {
            Debug.LogWarning("File \"" + filepath + "\" does not exist. Attempting to create from \"" +
                             Application.dataPath + "!/assets/" + p);
            // if it doesn't ->
            // open StreamingAssets directory and load the db -> 
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + p);
            while (!loadDB.isDone) { }
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDB.bytes);
        }

        //open db connection
        connection = "URI=file:" + filepath;
        Debug.Log("Stablishing connection to: " + connection);
        dbcon = new SqliteConnection(connection);
        dbcon.Open();
    }

    public void CloseDB()
    {
        reader.Close(); // clean everything up
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
    }

    public List<TestData> ItemList = new List<TestData>();
    public StringBuilder SingleSelectWhere()
    { // Selects a single Item
        string query;	
        query = "SELECT * FROM TestDB";
        dbcmd = dbcon.CreateCommand();
        dbcmd.CommandText = query;

        using (reader = dbcmd.ExecuteReader()) // 테이블에 있는 데이터들이 들어간다. 
        {
            //안에 데이터 전부를 읽어온다
            while (reader.Read())
            {          
                ItemList.Add(new TestData(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
            }
        }

        for (int i = 0; i < ItemList.Count; i++)
        {
            string data = ItemList[i].ID + "::" + ItemList[i].Hp + "::" + ItemList[i].MoveSpeed + "::" + ItemList[i].AttackPower + "\n";

            builder.Append(data);
        }

        return builder;
    }
  
}
