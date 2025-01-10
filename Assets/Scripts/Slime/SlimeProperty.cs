using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public enum SlimeState{
    Idle,
    Chat,
    Music,
    Acknowledge
}
public enum Emoji{
    Idle,
    CloseEye,
    Excited,
    Mad,
    Confused,
    emm
}

[System.Serializable]
public struct FaceMaterial{
    public Material idle;
    public Material closeEye;
    public Material mad;
    public Material excited;
    public Material confused;
    public Material emmm;
}


public class SlimeProperty : MonoBehaviour
{
    [Header("Property")]
    public SlimeState slimeState;
    public float moveSpeed;
    public float turnSpeed;
    public float turnSpeed_slow;
    public float jumpForce;
    public GroundCheck groundCheck;
    public Emoji emoji;
    public bool foundInteractTarget;
    public FloorGrid currGrid;      //no use? (old system)
    // public FloorState currFloorState;
    public GridDatum currGridDatum;

    [Header("Variables")]
    public FaceMaterial faceMaterial;
    public GameObject root;
    public FieldOfView fieldOfView;
    public GridManager gridManager;
    public SlimeEffect effect;

    // public bool isColliding;
    // public SlimeColliderCheck slimeColliderCheck;
    private void Awake() {
        gridManager = FindObjectOfType<GridManager>();
    }

    public Material EmojiToMaterial(Emoji emoji){
        switch (emoji){
            case Emoji.Idle:
                return faceMaterial.idle;
            case Emoji.CloseEye:
                return faceMaterial.closeEye;
            case Emoji.Excited:
                return faceMaterial.excited;
            case Emoji.Mad:
                return faceMaterial.mad;
            case Emoji.Confused:
                return faceMaterial.confused;
            case Emoji.emm:
                return faceMaterial.emmm; 
            default:
                return faceMaterial.idle;
        }
    }

    private void Update() {
        root.transform.position = new Vector3(transform.position.x, root.transform.position.y, transform.position.z);
        foundInteractTarget = fieldOfView.foundTarget;
        currGridDatum = gridManager.GetFloorGridDatum(new Vector3(transform.position.x, 1, transform.position.z));
    }

    public void Instantiate(float3 position, SlimeState state){
        transform.position = position;
        slimeState = state;
    }
    
}
