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

    //PlayerPref KeyValue
    public static string CharacterKeyValue = "CharacterType";
    public static string PassiveKeyValue = "Passive";

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
}

public static class CustomColor
{
    public static Color Orange = new Color(255f / 255f, 187f / 255f, 0f);
    public static Color Silver = new Color(192f / 255f, 192f / 255f, 192f/255f);
    public static Color SkinColor = new Color(255f / 255f, 215f / 255f, 181f / 255f);
}
