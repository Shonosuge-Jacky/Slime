using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public UIManager UIManager { get; private set; }
    public bool isControlable;
    public GameMode CurrGameMode;
    public bool CloseSettingPannel;

    GameObject m_OverheadCamera;
    GameObject m_PlayerGameObject;
    static GameObject m_OOPSlimeParent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        UIManager = GetComponentInChildren<UIManager>();
        // m_OverheadCamera = FindObjectOfType<OverheadCamera>().gameObject;
        m_PlayerGameObject = GameObject.FindGameObjectWithTag("Player");
        m_OOPSlimeParent = GameObject.FindGameObjectWithTag("SlimeParent");

    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        CurrGameMode = GameDataCenter._InitialGameMode;
    }
    


    public void ChangeGameMode(){
        UIManager.Instance.EnterLoadingUI();
        DOVirtual.DelayedCall(0.5f, ()=>{
            CurrGameMode = CurrGameMode == GameMode.Inspect? GameMode.Explore : GameMode.Inspect;
            switch(CurrGameMode)
            {
                case GameMode.Inspect:
                    EventCenter.Instance.BoardcastEvent(EventType.ChangeGameModeToInspect);
                    StartCoroutine(EventCenter.Instance.SendEventToECS(EventType.ChangeGameModeToInspect));
                    // m_PlayerGameObject.SetActive(true);
                    // m_OverheadCamera.SetActive(false);
                    break;
                case GameMode.Explore:
                    EventCenter.Instance.BoardcastEvent(EventType.ChangeGameModeToExplore);
                    StartCoroutine(EventCenter.Instance.SendEventToECS(EventType.ChangeGameModeToExplore));
                    break;
            }
        });
        
    }

    public static void CreateOOPGameObject(RefRO<SlimeComponent> slime, RefRO<LocalTransform> slimeTransform){
        Debug.Log(slime.ValueRO.CurrSubState);
        if(!m_OOPSlimeParent){
            Debug.LogError("No GameObject with Tag SlimeParent");
        }
        Instantiate(GameDataCenter._SlimePrefabOOP, m_OOPSlimeParent.transform).
            GetComponent<SlimeProperty>().Instantiate(slimeTransform.ValueRO.Position, slime.ValueRO.CurrState);
        
    }

}

public enum GameMode{
    Inspect,
    Explore,
    Phone
}