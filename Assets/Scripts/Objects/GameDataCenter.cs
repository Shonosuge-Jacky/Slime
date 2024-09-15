using System;
using UnityEngine;

// public struct 
public static class GameDataCenter{
    public static GameData _GameData;
    public static int test = 11111;

}

[Serializable]
public struct GameData{
    [Header("EnviornmentObject")]
    public FloorGridsStorage    _FloorGridStorage;
    public FloorSetting         _FloorSetting;
    public FloorGameObject      _Jukebox;
    public FloorGameObject      _StreetLight;
    [Header("ECSSetting")]
    public int                  _SlimeAmount;
    public int                  _RoomMinX;
    public int                  _RoomMinY;
    public int                  _RoomMaxX;
    public int                  _RoomMaxY;

    [Header("OOPPrefab")]
    public GameObject           _SlimePrefabOOP;
    
    [Header("ECSPrefab")]
    public GameObject           _SlimePrefabECS;

    [Header("SlimeProperty")]
    public float                _SlimeMoveSpeed;
    public float                _SlimeTurnSpeed;
    public float                _SlimeTurnSpeed_Slow;
    public float                _SlimeJumpForce;
    public SlimeState           _SlimeInitialState;
    public Emoji                _SlimeInitialEmoji;
    public FaceMaterial         _SlimeFaceMaterial;

};


// public static GameStaticData _GameStaticData;
