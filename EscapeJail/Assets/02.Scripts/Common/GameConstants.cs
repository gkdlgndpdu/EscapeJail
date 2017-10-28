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

    //상태이상 데미지
    public static int fireTicDamage = 1;
    public static int poisonTicDamage = 1;

    //PlayerPref KeyValue
    public static string CharacterKeyValue = "CharacterType";

    //DB
    public static string MonsterDBName = "MonsterDB.db";
    public static string WeaponDBName = "WeaponDB.db";
}
