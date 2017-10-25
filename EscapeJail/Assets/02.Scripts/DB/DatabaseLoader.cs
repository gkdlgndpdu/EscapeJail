using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Mono.Data.SqliteClient;

public class DatabaseLoader : MonoBehaviour
{
    private Dictionary<MonsterName, MonsterDB> MonsterDB;

    public static DatabaseLoader Instance = null;

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
        string connection;

#if UNITY_EDITOR
        filepath = Application.streamingAssetsPath + "/" + fileName;
#else
  string filepath = Application.persistentDataPath + "/" + fileName;
#endif    

        //open db connection
        connection = "URI=file:" + filepath;

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

#if UNITY_EDITOR
        //디버깅
        foreach (KeyValuePair<MonsterName, MonsterDB> data in MonsterDB)
        {
            Debug.Log("키:" + data.Key.ToString() + " 체력 :" + data.Value.Hp + " 확률 :" + data.Value.Probability);
        }
#endif

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
