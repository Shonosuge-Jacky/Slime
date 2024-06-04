
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [System.Serializable]
    public struct Debugs{
        public GameObject debugPrefab;
        public Material idle;
        public Material state;
        public Material arrow;
    }
    public Debugs debugs;
    Grid grid;
    public FloorGridsStorage floorGridsStorage;

    public Transform floorObjectParent;
    public bool showDebug;
    public DayNightEvent daynightEvent;

    [Header("Floor GameObject")]
    public FloorGameObject musicBoxObject;
    public FloorGameObject streeLightObject;
    

    private void Awake() {
        grid = GetComponent<Grid>();
        floorGridsStorage = new FloorGridsStorage();
    }

    private void Start() {
        InitialGridIndicator(1,1,100,100);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.K)){
            ShowDebug(1,1,100,100);
        }
    }

    void ShowDebug(int minX, int minZ, int maxX, int maxZ){
        showDebug = !showDebug;
        for(int i = minX; i<maxX; i++){
            for (int j = minZ; j<maxZ; j++){       
                floorGridsStorage.ActiveDebug(new Vector3Int(i, 0, j), showDebug);
            }
        }
    }

    void InitialGridIndicator(int minX, int minZ, int maxX, int maxZ){
        for(int i = minX; i<maxX; i++){
            for (int j = minZ; j<maxZ; j++){       
                GameObject debug = Instantiate(debugs.debugPrefab, grid.CellToWorld(new Vector3Int(i,0,j)) + new Vector3(0,0.1f,0), Quaternion.identity, transform);
                floorGridsStorage.AddFloorGrid(new Vector3Int(i,0,j), debug);
                if (!showDebug) debug.SetActive(false);
            }
        }
        AddObjectInGrid(20,20,musicBoxObject);
        AddObjectInGrid(20,60,musicBoxObject);
        AddObjectInGrid(80,40,musicBoxObject);
        AddObjectInGrid(10, 50, streeLightObject);
        AddObjectInGrid(70, 10, streeLightObject);
        AddObjectInGrid(70, 70, streeLightObject);
        floorGridsStorage.DictionaryToList();
        SetFloorBorder(100);
    }

    public void AddObjectInGrid(int x, int z, FloorGameObject floorGameObject){
        
        GameObject newEnvironemtObject = Instantiate(floorGameObject.gameObject, 
            grid.CellToWorld(new Vector3Int(x, 0, z)) + floorGameObject.positionOffset, 
            floorGameObject.rotationOffset, 
            floorObjectParent);
        daynightEvent.AddListener(newEnvironemtObject.GetComponent<EnvironmentObject>());

        for (int i = x - floorGameObject.leadArea ; i < x + floorGameObject.leadArea +1; i++){
            for (int j = z - floorGameObject.leadArea ; j < z + floorGameObject.leadArea +1; j++){
                if(i != x || j != z){
                    // Debug.Log("Move" + i + " " + j);
                    floorGridsStorage.SetFloorState(new Vector3Int(i, 0, j), FloorState.Move, FloorState.Idle, new Vector3(x,0,z) - new Vector3(i,0,j) );
                    floorGridsStorage.SetFloorDebug(new Vector3Int(i, 0, j), debugs.arrow, debugs.idle);
                    floorGridsStorage.AddFloorGridToDayNightEventListener(new Vector3Int(i, 0, j), daynightEvent);
                }
            }
        }
        
        for (int i = x - floorGameObject.stateArea; i < x + floorGameObject.stateArea+1; i++){
            for (int j = z - floorGameObject.stateArea; j < z + floorGameObject.stateArea+1; j++){
                // Debug.Log("Music" + i + " " + j);
                floorGridsStorage.SetFloorState(new Vector3Int(i, 0, j), FloorState.Music, FloorState.Idle);
                floorGridsStorage.SetFloorDebug(new Vector3Int(i, 0, j), debugs.state, debugs.idle);
                // floorGridsStorage.RemoveFloorGridToDayNightEventListener(new Vector3Int(i, 0, j), daynightEvent);
            }
        }
    }

    void SetFloorBorder(int max){
        for (int i = 1; i < max; i++){
            // Debug.Log(i);
            floorGridsStorage.SetFloorState(new Vector3Int(i, 0, 1), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(i,0,1) );
            floorGridsStorage.SetFloorDebug(new Vector3Int(i, 0, 1), debugs.arrow);
            floorGridsStorage.SetFloorState(new Vector3Int(i, 0, max-1), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(i,0,max-1) );
            floorGridsStorage.SetFloorDebug(new Vector3Int(i, 0, max-1), debugs.arrow);
            floorGridsStorage.SetFloorState(new Vector3Int(1, 0, i), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(1,0,i) );
            floorGridsStorage.SetFloorDebug(new Vector3Int(1, 0, i), debugs.arrow);
            floorGridsStorage.SetFloorState(new Vector3Int(max-1, 0, i), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(max-1,0,i) );
            floorGridsStorage.SetFloorDebug(new Vector3Int(max-1, 0, i), debugs.arrow);
            
            floorGridsStorage.SetFloorState(new Vector3Int(i, 0, 2), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(i,0,2) );
            floorGridsStorage.SetFloorDebug(new Vector3Int(i, 0, 2), debugs.arrow);
            floorGridsStorage.SetFloorState(new Vector3Int(i, 0, max-2), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(i,0,max-2) );
            floorGridsStorage.SetFloorDebug(new Vector3Int(i, 0, max-2), debugs.arrow);
            floorGridsStorage.SetFloorState(new Vector3Int(2, 0, i), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(2,0,i) );
            floorGridsStorage.SetFloorDebug(new Vector3Int(2, 0, i), debugs.arrow);
            floorGridsStorage.SetFloorState(new Vector3Int(max-2, 0, i), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(max-2,0,i) );
            floorGridsStorage.SetFloorDebug(new Vector3Int(max-2, 0, i), debugs.arrow);

            floorGridsStorage.SetFloorState(new Vector3Int(i, 0, 3), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(i,0,3) );
            floorGridsStorage.SetFloorDebug(new Vector3Int(i, 0, 3), debugs.arrow);
            floorGridsStorage.SetFloorState(new Vector3Int(i, 0, max-3), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(i,0,max-3) );
            floorGridsStorage.SetFloorDebug(new Vector3Int(i, 0, max-3), debugs.arrow);
            floorGridsStorage.SetFloorState(new Vector3Int(3, 0, i), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(3,0,i) );
            floorGridsStorage.SetFloorDebug(new Vector3Int(3, 0, i), debugs.arrow);
            floorGridsStorage.SetFloorState(new Vector3Int(max-3, 0, i), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(max-3,0,i) );
            floorGridsStorage.SetFloorDebug(new Vector3Int(max-3, 0, i), debugs.arrow);
        }


    }

    public FloorGrid GetFloorGrid(Vector3 position){
        return floorGridsStorage.GetFloorGrid(grid.WorldToCell(position));
    }

}