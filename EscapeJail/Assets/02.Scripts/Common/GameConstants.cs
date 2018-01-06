using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants
{
    //레이어
    public static int PlayerLayerMin = 20;
    public static int TileLayerMin = 0;
    public static int WallLayerMin = 2;
    public static int BackgroundLayerMin = -2;
    public static int ArticleLayerMin = 3;

    //타일크기
    public static float tileSize = 0.64f;
    //맵간 밀릴때 최소거리
    public static float eachModuleDistance = 1.28f;
    //상태이상 데미지
    public static int fireTicDamage = 1;
    public static int poisonTicDamage = 1;

 

    //진통제 지속시간
    public static float StimulantDurationTime = 30f;

    //섬광탄 스턴 지속시간
    public static float FlashBangStunTime = 3f;

    //밀리는 시간
    public static float PushTime = 0.5f;

    //DB

    public static string MonsterDBName = "MonsterDB.db";
    public static string ItemDBName = "ItemDB.db";
    public static string ItemProbabilityDBName = "ItemProbabilityDB.db";
    public static string WeaponDBName = "WeaponDB.db";
    public static string PassiveDBName = "PassiveItemDB.db";
    public static string CharacterDBName = "CharacterDB.db";
    public static string LocalizationDBName = "Localization.db";
    //재화
    public static int CoinGetValue = 100;
    public static int MedalGetValue = 50;

 

    public static float ReboundDecreaseValue = 5f;

    public static int DefaultBagSize = 5;
    public static int ItemOverlapGold = 100;

    public static int lastStageLevel = 5;
    
}

public static class ScorePoint
{
    //점수
    public static int EnemyPoint = 50;
    public static int BossPoint = 1000;
    public static int StagePoint = 1000;
    // public static int HeartMinus = 500;
    public static int HeartMinus = 100;

    public static int ClearRoom = 100;
}

public static class Probability
{
    //무기상자 생성확률
    public static int weaponBoxProb = 10;
    public static int vampireicPassive = 1;
}
public static class GoodsValue
{
    public static int MedalPoint = 10;
    
}

public static class CustomColor
{
    public static Color Orange = new Color(255f / 255f, 187f / 255f, 0f);
    public static Color Silver = new Color(192f / 255f, 192f / 255f, 192f/255f);
    public static Color SkinColor = new Color(255f / 255f, 215f / 255f, 181f / 255f);
}

public static class PlayerPrefKeys
{

    public static string CharacterKeyValue = "CharacterType";


    public static string BgmMuteKey ="BgmMute"; //0 false 1 true
    public static string EffectMuteKey ="EffectMute"; //0 false 1 true
    public static string BgmVolumeKey = "BgmVolume";
    public static string EffectVolumeKey = "EffectVolume";
    public static string LanguageKey = "Language"; //0한글 1영어

    public static string MoveStickTypeKey = "MoveStickType"; //0고정 1유동
    public static string VibrationKey = "VibrationKey"; //0 off 1 on
    public static string PassiveKey = "Passive";
    public static string FirstPlayKey = "FirstPlay"; //0 false 1 true
    public static string HardUnlockKey = "HardModeKey"; //0 lock 1 unlock
}
