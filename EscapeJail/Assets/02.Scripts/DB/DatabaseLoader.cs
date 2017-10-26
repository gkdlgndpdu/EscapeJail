using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Mono.Data.SqliteClient;
using UnityEngine.UI;
using System.Text;
public class DatabaseLoader : MonoBehaviour
{
    private Dictionary<MonsterName, MonsterDB> MonsterDB;

    public static DatabaseLoader Instance = null;

    //디버그용
    public Text debugText;
    public StringBuilder sb = new StringBuilder();
        

    public MonsterDB GetMonsterData(MonsterName key)
    {
        if (MonsterDB == null) return null;
        if (MonsterDB.ContainsKey(key) == false) return null;

        return MonsterDB[key];
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        LoadMonsterDB();
        DontDestroyOnLoad(this.gameObject);
    }

    private void LoadMonsterDB()
    {
        MonsterDB = new Dictionary<MonsterName, MonsterDB>();

        string fileName = GameConstants.MonsterDBName;
        IDbCommand dbcmd = null;
        IDbConnection dbcon = null;
        IDataReader reader = null;

        OpenDB(fileName, ref dbcon);
        ReadItemDB(ref dbcmd, ref dbcon, ref reader);
        CloseDB(ref dbcmd, ref dbcon, ref reader);

    }

    public void OpenDB(string fileName, ref IDbConnection dbcon)
    {
        string filepath;
#if UNITY_EDITOR
        filepath = Application.streamingAssetsPath + "/" + fileName;
#else
       filepath = Application.persistentDataPath + "/" + fileName;
#endif



        //filepath = Application.streamingAssetsPath + "/" + fileName;



        if (!File.Exists(filepath))
        {
            Debug.LogWarning("File \"" + filepath + "\" does not exist. Attempting to create from \"" +
                             Application.dataPath + "!/assets/" + fileName);
            // if it doesn't ->
            // open StreamingAssets directory and load the db -> 
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + fileName);
            while (!loadDB.isDone) { }
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDB.bytes);
        }


        //open db connection
        string connection = "URI=file:" + filepath;

        dbcon = new SqliteConnection(connection);
        dbcon.Open();
    }

    public void ReadItemDB(ref IDbCommand dbcmd, ref IDbConnection dbcon, ref IDataReader reader)
    { // Selects a single Item
        string query;
        /////////////////////////////////////////////////////////// 반드시 수정
        query = "SELECT * FROM MonsterDB";
        ////////////////////////////////////////////////////////// 반드시 수정
        dbcmd = dbcon.CreateCommand();
        dbcmd.CommandText = query;

        using (reader = dbcmd.ExecuteReader()) // 테이블에 있는 데이터들이 들어간다. 
        {
            //안에 데이터 전부를 읽어온다
            while (reader.Read())
            {
                MonsterDB.Add((MonsterName)reader.GetInt32(0), new MonsterDB(reader.GetInt32(1), reader.GetInt32(2)));
            }
        }


        //디버깅
        foreach (KeyValuePair<MonsterName, MonsterDB> data in MonsterDB)
        {
            string debugmsg = "키:" + data.Key.ToString() + " 체력 :" + data.Value.Hp + " 확률 :" + data.Value.Probability + "\n";
            Debug.Log(debugmsg);
            sb.Append(debugmsg);
        }

        if(debugText!=null)
        debugText.text = sb.ToString();

    }


    public void CloseDB(ref IDbCommand dbcmd, ref IDbConnection dbcon, ref IDataReader reader)
    {
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
    }
}
