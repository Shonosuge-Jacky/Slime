using System;
using UnityEngine;

// public struct 
public struct GameDataCenter
{
    [Header("GameSetting")]
    public static GameMode              _InitialGameMode        = GameMode.Inspect;
    [Header("EnviornmentObject")]
    public static FloorGridsStorage     _FloorGridStorage       = Resources.Load<FloorGridsStorage>("ScriptableObjects/FloorGridStorage");
    public static FloorSetting          _FloorSetting           = Resources.Load<FloorSetting>("ScriptableObjects/FloorSetting");
    public static FloorGameObject       _Jukebox                = Resources.Load<FloorGameObject>("ScriptableObjects/MusicBox");
    public static FloorGameObject       _StreetLight            = Resources.Load<FloorGameObject>("ScriptableObjects/StreetLight");

    [Header("RoomSetting")]
    public static FloorState            _FloorInitialState      = FloorState.Idle;

    [Header("ECSSetting")]
    public static int                   _SlimeAmount            = 100;

    [Header("OOPPrefab")]
    public static GameObject            _SlimePrefabOOP         = Resources.Load<GameObject>("Prefabs/SlimePrefabOOP");
    public static GameObject            _PlayerPrefab           = Resources.Load<GameObject>("Prefabs/PlayerPrefab");
    
    [Header("ECSPrefab")]
    public static GameObject            _SlimePrefabECS         = Resources.Load<GameObject>("Prefabs/SlimePrefabECS");
    public static GameObject            _EmptyPrefabECS         = Resources.Load<GameObject>("Prefabs/EmptyPrefabECS");

    [Header("SlimeProperty")]
    public static float                 _SlimeMoveSpeed         = 1f;
    public static float                 _SlimeTurnSpeed         = 90f;
    public static float                 _SlimeTurnSpeed_Slow    = 30f;
    public static float                 _SlimeJumpForce         = 40f;
    public static SlimeState            _SlimeInitialState      = SlimeState.Idle;
    public static Emoji                 _SlimeInitialEmoji      = Emoji.Idle;
    public static FaceMaterial          _SlimeFaceMaterial      = new FaceMaterial
    {
        idle        = Resources.Load<Material>("Texture/idle"),
        closeEye    = Resources.Load<Material>("Texture/cloeEye"),
        mad         = Resources.Load<Material>("Texture/mad"),
        excited     = null,
        confused    = null,
        emmm        = null
    };

    // [Header("PlayerProperty")]



    public static FloorGameObject GetFloorGameObject(FloorGameObjectType type){
        switch(type){
            case FloorGameObjectType.Jukebox:
            return _Jukebox;
            
            case FloorGameObjectType.StreetLight:
            return _StreetLight;
        }
        return null;
    }
}
