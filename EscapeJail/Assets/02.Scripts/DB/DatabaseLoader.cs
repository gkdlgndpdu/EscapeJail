using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Mono.Data.SqliteClient;
using UnityEngine.UI;
using weapon;
/// <summary>
/// DB 파일을 읽어오고 정보를 저장
/// </summary>

public class DatabaseLoader : MonoBehaviour
{
    //몬스터 DB
    private Dictionary<MonsterName, MonsterDB> MonsterDB;
    
    //아이템 DB
    //대분류
    private Dictionary<ItemType, ItemProbabilityDB> ItemProbabilityDB;
    //중분류
    private Dictionary<string, ItemDB> ItemDB;

    //무기 DB
    private Dictionary<WeaponType,WeaponDB> WeaponDB;

    /// <summary>
    /// key값에 해당 아이템을 넣으면 확률에 맞게 lv을 뱉음
    /// </summary>
    private Dictionary<string, RandomGenerator<int>> ItemRandomLevelGenerator;

    public static DatabaseLoader Instance = null;

    private RandomGenerator<WeaponType> WeaponRandomGenerator;

    //디버그용
    public Text debugText;
    public StringBuilder sb = new StringBuilder();

    public int RandomItemLevelGenerator(string key)
    {
        if (ItemRandomLevelGenerator == null) return 0;
        if (ItemRandomLevelGenerator.ContainsKey(key) == false) return 0;
        return ItemRandomLevelGenerator[key].GetRandomData();
    }
        
    public WeaponType GetRandomWeaponTypeByProbability()
    {
        if (WeaponRandomGenerator == null) return WeaponType.Revolver;
        if (WeaponRandomGenerator.CanReturnValue() == false) return WeaponType.Revolver;
        WeaponType randWeaponType = WeaponRandomGenerator.GetRandomData();
        WeaponRandomGenerator.RemoveInList(randWeaponType);
        return randWeaponType;
    }

    public MonsterDB GetMonsterData(MonsterName key)
    {
        if (MonsterDB == null) return null;
        if (MonsterDB.ContainsKey(key) == false) return null;

        return MonsterDB[key];
    }

    public ItemDB GetItemDBData(string key)
    {
        if (ItemDB == null) return null;
        if (ItemDB.ContainsKey(key) == false) return null;

        return ItemDB[key];
    }

    public ItemProbabilityDB GetItemProbabilityDB(ItemType key)
    {
        if (ItemProbabilityDB == null) return null;
        if (ItemProbabilityDB.ContainsKey(key) == false) return null;

        return ItemProbabilityDB[key];
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

        LoadAllDB();

        DontDestroyOnLoad(this.gameObject);
    }

    private void LoadAllDB()
    {
        LoadMonsterDB();
        LoadItemDB();
        LoadItemProbabilityDB();
        LoadWeaponProbabilityDB();

        DebugDB();
    }

    private void DebugDB()
    {
        if (debugText != null)
            debugText.text = sb.ToString();
    }

    private void LoadMonsterDB()
    {
        MonsterDB = new Dictionary<MonsterName, MonsterDB>();

        string fileName = GameConstants.MonsterDBName;
        IDbCommand dbcmd = null;
        IDbConnection dbcon = null;
        IDataReader reader = null;

        OpenDB(fileName, ref dbcon);
        ReadMonsterDB(ref dbcmd, ref dbcon, ref reader);
        CloseDB(ref dbcmd, ref dbcon, ref reader);

    }

    private void LoadItemDB()
    {
        ItemDB = new Dictionary<string, ItemDB>();
        ItemRandomLevelGenerator = new Dictionary<string, RandomGenerator<int>>();

        string fileName = GameConstants.ItemDBName;
        IDbCommand dbcmd = null;
        IDbConnection dbcon = null;
        IDataReader reader = null;

        OpenDB(fileName, ref dbcon);
        ReadItemDB(ref dbcmd, ref dbcon, ref reader);
        CloseDB(ref dbcmd, ref dbcon, ref reader);

        
    }

    private void LoadItemProbabilityDB()
    {
        ItemProbabilityDB = new Dictionary<ItemType, ItemProbabilityDB>();

        string fileName = GameConstants.ItemProbabilityDBName;
        IDbCommand dbcmd = null;
        IDbConnection dbcon = null;
        IDataReader reader = null;

        OpenDB(fileName, ref dbcon);
        ReadItemProbabilityDB(ref dbcmd, ref dbcon, ref reader);
        CloseDB(ref dbcmd, ref dbcon, ref reader);
    }

    private void LoadWeaponProbabilityDB()
    {
        WeaponDB = new Dictionary<WeaponType, WeaponDB>();
        WeaponRandomGenerator = new RandomGenerator<WeaponType>();

        string fileName = GameConstants.WeaponDBName;
        IDbCommand dbcmd = null;
        IDbConnection dbcon = null;
        IDataReader reader = null;

        OpenDB(fileName, ref dbcon);
        ReadWeaponDB(ref dbcmd, ref dbcon, ref reader);
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

    public void CloseDB(ref IDbCommand dbcmd, ref IDbConnection dbcon, ref IDataReader reader)
    {
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;

    
    }


    //몬스터 정보 저장
    public void ReadMonsterDB(ref IDbCommand dbcmd, ref IDbConnection dbcon, ref IDataReader reader)
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
                int t = reader.FieldCount;
                MonsterDB.Add((MonsterName)reader.GetInt32(0), new MonsterDB(reader.GetInt32(1), reader.GetInt32(2)));
             
            }
        }

        //디버깅
        foreach (KeyValuePair<MonsterName, MonsterDB> data in MonsterDB)
        {
            string debugmsg = "Key:" + data.Key.ToString() + " Hp :" + data.Value.Hp + " Prob :" + data.Value.Probability + "\n";
            Debug.Log(debugmsg);
            sb.Append(debugmsg);
        }

    }


    //테이블
    // 0   1   2   3    4       5
    //key lv1 lv2 lv3 value discription


    private void ReadItemDB(ref IDbCommand dbcmd, ref IDbConnection dbcon, ref IDataReader reader)
    {
        string query;
        /////////////////////////////////////////////////////////// 반드시 수정
        query = "SELECT * FROM ItemDB";
        ////////////////////////////////////////////////////////// 반드시 수정
        dbcmd = dbcon.CreateCommand();
        dbcmd.CommandText = query;

        using (reader = dbcmd.ExecuteReader()) // 테이블에 있는 데이터들이 들어간다. 
        {
            //안에 데이터 전부를 읽어온다
            while (reader.Read())
            {                
                //아이템일반정보
                ItemDB.Add(reader.GetString(0), new ItemDB(reader.GetInt32(4), reader.GetString(5)));

                //레벨별 확률정보
                RandomGenerator<int> randGenerator = new RandomGenerator<int>();
                for(int i = 1; i < 4; i++)
                {
                    randGenerator.AddToList(i, reader.GetInt32(i));
                }
                ItemRandomLevelGenerator.Add(reader.GetString(0), randGenerator);

            }
        }
    }



    private void ReadItemProbabilityDB(ref IDbCommand dbcmd, ref IDbConnection dbcon, ref IDataReader reader)
    {
        string query;
        /////////////////////////////////////////////////////////// 반드시 수정
        query = "SELECT * FROM ItemProbabilityDB";
        ////////////////////////////////////////////////////////// 반드시 수정
        dbcmd = dbcon.CreateCommand();
        dbcmd.CommandText = query;

        using (reader = dbcmd.ExecuteReader()) // 테이블에 있는 데이터들이 들어간다. 
        {
            //안에 데이터 전부를 읽어온다
            while (reader.Read())
            {
                ItemProbabilityDB.Add((ItemType)reader.GetInt32(0), new ItemProbabilityDB(reader.GetInt32(1), reader.GetString(2)));

            }
        }


        //디버깅
        foreach (KeyValuePair<ItemType, ItemProbabilityDB> data in ItemProbabilityDB)
        {
            string debugmsg = "Key:" + data.Key.ToString() + " Prob :" + data.Value.Probability + " Desc :" + data.Value.Description + "\n";
            Debug.Log(debugmsg);
            sb.Append(debugmsg);
        }

    }

    private void ReadWeaponDB(ref IDbCommand dbcmd, ref IDbConnection dbcon, ref IDataReader reader)
    {
        string query;
        /////////////////////////////////////////////////////////// 반드시 수정
        query = "SELECT * FROM WeaponDB";
        ////////////////////////////////////////////////////////// 반드시 수정
        dbcmd = dbcon.CreateCommand();
        dbcmd.CommandText = query;

        using (reader = dbcmd.ExecuteReader()) // 테이블에 있는 데이터들이 들어간다. 
        {
            //안에 데이터 전부를 읽어온다
            while (reader.Read())
            {                                                               //Probability       //Description
                WeaponDB.Add((WeaponType)reader.GetInt32(0), new WeaponDB(reader.GetInt32(1), reader.GetString(2)));
                WeaponRandomGenerator.AddToList((WeaponType)reader.GetInt32(0), reader.GetInt32(1));
            }
        }


        //디버깅
        foreach (KeyValuePair<WeaponType, WeaponDB> data in WeaponDB)
        {
            string debugmsg = "Key:" + data.Key.ToString() + " Prob :" + data.Value.Probability + " Desc :" + data.Value.Description + "\n";
            Debug.Log(debugmsg);
            sb.Append(debugmsg);
        }

    }


}
