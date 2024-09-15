using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public UIManager UIManager { get; private set; }

    public GameData InitializationGameData;
    public bool isControlable;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        UIManager = GetComponentInChildren<UIManager>();
    }

    void InitialGameData(GameData m_GameData){
        GameDataCenter._GameData = m_GameData;
    }

}
