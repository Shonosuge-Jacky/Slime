using System;
using System.Collections.Generic;
using UnityEngine;

public enum FloorState{
    Move,
    Idle,
    Music
}

[Serializable]
public class FloorGrid{
    public Vector3Int cellPosition;
    public FloorState floorState;
    public Quaternion direction;
    public GameObject debug;
    public FloorGrid(Vector3Int cellPosition, FloorState floorState, Quaternion direction, GameObject debug){
        this.cellPosition = cellPosition;
        this.floorState = floorState;
        this.direction = direction;
        this.debug = debug;
    }

    public void SetFloorState(FloorState floorState){
        this.floorState = floorState;
    }
    public void SetDirection(Quaternion direction){
        this.direction = direction;
    }
}

[CreateAssetMenu(fileName = "storage", menuName = "ScriptableObjects/FloorStatesStorage")]
public class FloorGridsStorage : ScriptableObject
{
    Dictionary<Vector3Int, FloorGrid> floorStates = new Dictionary<Vector3Int, FloorGrid>();
    [SerializeField] List<FloorGrid> floorStates_List = new List<FloorGrid>();

    public void AddFloorGrid(Vector3Int cellPosition, GameObject debug){
        floorStates.Add(cellPosition, new FloorGrid(cellPosition, FloorState.Idle, Quaternion.identity, debug));
    }
    public FloorGrid GetFloorGrid(Vector3Int cellPosition){
        return floorStates[cellPosition];
    }

    public FloorState GetFloorState(Vector3Int cellPosition){
        return floorStates[cellPosition].floorState;
    }
    public void SetFloorState(Vector3Int cellPosition, FloorState state){
        floorStates[cellPosition].floorState = state;
        floorStates[cellPosition].direction = Quaternion.identity;
    }
    public void SetFloorState(Vector3Int cellPosition, Vector3 direction){
        floorStates[cellPosition].floorState = FloorState.Move;
        floorStates[cellPosition].direction = Quaternion.LookRotation(direction);
    }
    
    public Quaternion GetFloorDirection(Vector3Int cellPosition){
        return floorStates[cellPosition].direction;
    }

    
    public void SetFloorDebug(Vector3Int cellPosition, Material material){
        floorStates[cellPosition].debug.GetComponent<Renderer>().material = material;
        if(floorStates[cellPosition].floorState == FloorState.Move){
            floorStates[cellPosition].debug.transform.rotation 
                = GetFloorDirection(cellPosition) * Quaternion.Euler(0, 180f, 0);
        }
        
    }

    public void ActiveDebug(Vector3Int cellPosition, bool isActive){
        floorStates[cellPosition].debug.SetActive(isActive);
    }

    public void DictionaryToList(){
        foreach(KeyValuePair<Vector3Int, FloorGrid> entry in floorStates){
            floorStates_List.Add(entry.Value);
        }
    }
}



