using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;



public class GoogleCloudSave : MonoBehaviour
{
    public static GoogleCloudSave instance;
    [HideInInspector]
    public string saveData;


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    /// <summary>
    /// ///////////////////////////////////////////
    /// </summary>
    private void Start()
    {
        //Init();
    }
    /// <summary>
    /// //////////////////////////////////////////
    /// </summary>
    public void Init()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

    }
    public void Login()
    {
        GoogleService.Instance.SignIn();
    }
    public bool CheckLogin()
    {
        return PlayGamesPlatform.Instance.IsAuthenticated();
    }

    #region 실제사용 하는 부분
    public void SaveMyPassivies()
    {
        if (CheckLogin() == false) return;
        //저장
        string data = string.Empty;

        for (int i = 0; i < (int)PassiveType.PassiveEnd; i++)
        {
            PassiveType passiveType = (PassiveType)i;
            int value = PlayerPrefs.GetInt(passiveType.ToString(), 0);
            if (i == 0)
            {
                data += value.ToString();
            }
            else if (i != 0)
            {
                data += "," + value.ToString();
            }
        }

        this.SaveToCloud(data);

    }
    public void LoadPassiveFromCloud()
    {
        if (CheckLogin() == false) return;
        this.LoadFromCloud();
    }

    //세이브 읽어와서 실제로 등록하는 부분
    public void ApplyData(string data)
    {
        string[] split = data.Trim().Split(',');
        //
        // 
        if (split.Length != 0)
        {
            for (int i = 0; i < split.Length; i++)
            {
                PassiveType passiveType = (PassiveType)i;
                PlayerPrefs.SetInt(passiveType.ToString(), int.Parse(split[i]));
            }
        }
        else if (split.Length == 0)
        {

        }
      

        DatabaseLoader.Instance.SetPrefPassiveData();

        //
    }

    #endregion

    #region Save
    public void SaveToCloud(string data)
    {
        Save(data);
    }

    private void Save(string data)
    {
        //로그인 안됐으면 나감
        if (CheckLogin() == false) return;  

      
        string id = Social.localUser.id;
        string fileName = string.Format("{0}_DATA", id);
        saveData = data;
        OpenSavedGame(fileName, true);


    }
    void OpenSavedGame(string fileName, bool saved)
    {
        ISavedGameClient savedClient = PlayGamesPlatform.Instance.SavedGame;

        if (saved == true)
        {
            savedClient.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpenedToSave);


        }
        else if (saved == false)
        {
            savedClient.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpenedToRead);
        }
    }
    void OnSavedGameOpenedToSave(SavedGameRequestStatus status, ISavedGameMetadata data)
    {
        //요청 성공시 byte 배열로 변환해 현재 시간과 함께 넘김
        if (status == SavedGameRequestStatus.Success)
        {
            byte[] b = Encoding.UTF8.GetBytes(string.Format(saveData));
            SaveGame(data, b, DateTime.Now.TimeOfDay);
        }
        else
        {
            Debug.Log("Fail");
        }
    }
    void SaveGame(ISavedGameMetadata data, byte[] _byte, TimeSpan playTime)
    {
        ISavedGameClient savedClient = PlayGamesPlatform.Instance.SavedGame;
        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();

        builder = builder.WithUpdatedPlayedTime(playTime).WithUpdatedDescription("Saved at " + DateTime.Now);

        SavedGameMetadataUpdate updateData = builder.Build();
        savedClient.CommitUpdate(data, updateData, _byte, OnSavedGameWritten);


    }
    //최종 저장 확인
    void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata data)
    {   
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Save Complete");
        }
        else
        {
            Debug.Log("Save Fail");
        }
    }


    #endregion

    #region Load
    public void LoadFromCloud()
    {
       Load();
    }


    void Load()
    {
        //로그인 안됐으면 나감
        if (CheckLogin()== false) return;

        Debug.Log("Try to load data");

        string id = Social.localUser.id;
        string fileName = string.Format("{0}_DATA", id);

        //이건 로드니까 두번째 인자 false
        OpenSavedGame(fileName, false);

    }
    void OnSavedGameOpenedToRead(SavedGameRequestStatus status, ISavedGameMetadata data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            LoadGameData(data);
        }
        else
        {
            Debug.Log("Fail");
        }
    }

    void LoadGameData(ISavedGameMetadata data)
    {
        ISavedGameClient savedClient = PlayGamesPlatform.Instance.SavedGame;
        savedClient.ReadBinaryData(data, OnSavedGameDataRead);
    }

    void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] _byte)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            string data = Encoding.Default.GetString(_byte);
            ApplyData(data);
        }
        else
        {
            Debug.Log("Load Fail");
        }
    }
    #endregion

}
