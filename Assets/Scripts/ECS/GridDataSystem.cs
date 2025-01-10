using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public partial struct GridDataSystem : ISystem
{

    // public event EventHandler OnShoot;
    static NativeHashMap<int2, GridDatum> Int2ToFloorState;
    
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SpawnerConfig>();
    }
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;
        Int2ToFloorState = 
            new NativeHashMap<int2, GridDatum>(1, Allocator.Persistent);
        for(int i = GameDataCenter._FloorSetting.MinX; i <= GameDataCenter._FloorSetting.MaxX; i++){
            for(int j = GameDataCenter._FloorSetting.MinY; j <= GameDataCenter._FloorSetting.MaxY; j++){
                Int2ToFloorState.Add(new int2(i, j), new GridDatum(GameDataCenter._FloorInitialState));
            }
        }

        // Debug.Log("Test: " + Int2ToFloorState[new int2(20, 20)].State.ToString());
        
        // Int2ToFloorState[new int2(20, 20)].SetValues(FloorState.Move, FloorState.Move);
        //ToDo: Test if its correct or not
        foreach(FloorObjectSetting s in GameDataCenter._FloorSetting.FloorObjects)
        {
            // Debug.Log(s.FloorGameObjectType);
            FloorGameObject o = GameDataCenter.GetFloorGameObject(s.FloorGameObjectType);
            Debug.Log("Instantiate GridData for " + GameDataCenter.GetFloorGameObject(s.FloorGameObjectType).gameObjectName + " AT " + s.X.ToString() + "," + s.Y.ToString());
            
            for(int i = s.X - o.leadArea; i < s.X + o.leadArea; i ++)
            {
                for(int j = s.Y - o.leadArea; j < s.Y + o.leadArea; j ++)
                {
                    NativeHashMapFunctions.ChangeValue(Int2ToFloorState, new int2(i, j), 
                        new GridDatum(FloorState.Move, FloorState.Idle, new Vector3(s.X,0,s.Y) - new Vector3(i,0,j)));
                }
            }

            for(int i = s.X - o.stateArea; i < s.X + o.stateArea; i ++)
            {
                for(int j = s.Y - o.stateArea; j < s.Y + o.stateArea; j ++)
                {
                    NativeHashMapFunctions.ChangeValue(Int2ToFloorState, new int2(i, j), 
                        new GridDatum(o.daytimeFloorState, o.nighttimeFloorState));
                }
            }
        }

        SetBorder(Int2ToFloorState);
        // Debug.Log("Test2: " + Int2ToFloorState[new int2(20, 20)].State.ToString());
        // Debug.Log("Test2: " + Int2ToFloorState[new int2(21, 21)].State.ToString());

        SpawnerConfig spawnerConfig = SystemAPI.GetSingleton<SpawnerConfig>();
        Entity entity = state.EntityManager.Instantiate(spawnerConfig.EmptyPrefab);
        state.EntityManager.AddComponent<GridData>(entity);
        SystemAPI.SetComponent(entity, new GridData{
            Int2ToFloorState = Int2ToFloorState
        });
        Debug.Log("DoneCreatingInt2ToFloorState" );
        // Debug.Log(Int2ToFloorState[new int2(20,21)].State);
    }

    void SetBorder(NativeHashMap<int2, GridDatum> Int2ToFloorState, int borderRange = 15){
        int max = GameDataCenter._FloorSetting.MaxX;
        int min = GameDataCenter._FloorSetting.MinX;
        Vector3 midPoint = new Vector3(max/2,0,max/2);
        for(int i = min - 1; i >= min - borderRange - 1; i--){
            for(int j = min - borderRange - 1; j <= max + borderRange; j++){
                Int2ToFloorState.Add( new int2(i, j), new GridDatum(FloorState.Move, midPoint - new Vector3(i, 0, j)));
            }
        }
        for(int i = min; i <= max; i ++){
            for(int j = 1; j <= borderRange; j ++){
                Int2ToFloorState.Add( new int2(i, min - j), new GridDatum(FloorState.Move, midPoint - new Vector3(i, 0, 0 - j)));
                Int2ToFloorState.Add( new int2(i, max + j), new GridDatum(FloorState.Move, midPoint - new Vector3(i, 0, 100 + j)));
            }
        }
        for(int i = max + 1 ; i <= max + borderRange + 1; i++){
            for(int j = min - borderRange - 1; j <= max + borderRange; j++){
                Int2ToFloorState.Add( new int2(i, j), new GridDatum(FloorState.Move, midPoint - new Vector3(i, 0, j)));
            }
        }

        // for (int i = -borderRange; i <= max + borderRange; i++)
        // {
        //     // for (int j = 0; j < 5; j++){
        //         Int2ToFloorState.Add( new int2(i, j), new GridDatum(FloorState.Move, midPoint - new Vector3(i, 0, j)));
        //         Int2ToFloorState.Add( new int2(i, max-j), new GridDatum(FloorState.Move, midPoint - new Vector3(i, 0, max-j)));
        //         Int2ToFloorState.Add( new int2(j, i), new GridDatum(FloorState.Move, midPoint - new Vector3(j, 0, i)));
        //         Int2ToFloorState.Add( new int2(max-j, i), new GridDatum(FloorState.Move, midPoint - new Vector3(max-j, 0, i)));
            // }
            

            // NativeHashMapFunctions.ChangeValue(Int2ToFloorState, new int2(i, 2), new GridDatum(FloorState.Move, midPoint - new Vector3(i, 0, 2)));
            // NativeHashMapFunctions.ChangeValue(Int2ToFloorState, new int2(i, max-2), new GridDatum(FloorState.Move, midPoint - new Vector3(i, 0, max-2)));
            // NativeHashMapFunctions.ChangeValue(Int2ToFloorState, new int2(2, i), new GridDatum(FloorState.Move, midPoint - new Vector3(2, 0, i)));
            // NativeHashMapFunctions.ChangeValue(Int2ToFloorState, new int2(max-2, i), new GridDatum(FloorState.Move, midPoint - new Vector3(max-2, 0, i)));

            // NativeHashMapFunctions.ChangeValue(Int2ToFloorState, new int2(i, 3), new GridDatum(FloorState.Move, midPoint - new Vector3(i, 0, 3)));
            // NativeHashMapFunctions.ChangeValue(Int2ToFloorState, new int2(i, max-3), new GridDatum(FloorState.Move, midPoint - new Vector3(i, 0, max-3)));
            // NativeHashMapFunctions.ChangeValue(Int2ToFloorState, new int2(3, i), new GridDatum(FloorState.Move, midPoint - new Vector3(3, 0, i)));
            // NativeHashMapFunctions.ChangeValue(Int2ToFloorState, new int2(max-3, i), new GridDatum(FloorState.Move, midPoint - new Vector3(max-3, 0, i)));
        // }

        // for (int i = 1; i < max; i++){
        //     // Debug.Log(i);
        //     NativeHashMapFunctions.ChangeValue(Int2ToFloorState, new int2(i, 1), new GridDatum(FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(i,0,1)));
        //     floorGridsStorage.SetFloorState(new Vector3Int(i, 0, 1), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(i,0,1) );
        //     floorGridsStorage.SetFloorState(new Vector3Int(i, 0, max-1), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(i,0,max-1) );
        //     floorGridsStorage.SetFloorState(new Vector3Int(1, 0, i), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(1,0,i) );
        //     floorGridsStorage.SetFloorState(new Vector3Int(max-1, 0, i), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(max-1,0,i) );
            
        //     floorGridsStorage.SetFloorState(new Vector3Int(i, 0, 2), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(i,0,2) );
        //     floorGridsStorage.SetFloorState(new Vector3Int(i, 0, max-2), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(i,0,max-2) );
        //     floorGridsStorage.SetFloorState(new Vector3Int(2, 0, i), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(2,0,i) );
        //     floorGridsStorage.SetFloorState(new Vector3Int(max-2, 0, i), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(max-2,0,i) );

        //     floorGridsStorage.SetFloorState(new Vector3Int(i, 0, 3), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(i,0,3) );
        //     floorGridsStorage.SetFloorState(new Vector3Int(i, 0, max-3), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(i,0,max-3) );
        //     floorGridsStorage.SetFloorState(new Vector3Int(3, 0, i), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(3,0,i) );
        //     floorGridsStorage.SetFloorState(new Vector3Int(max-3, 0, i), FloorState.Move, new Vector3(max/2,0,max/2) - new Vector3(max-3,0,i) );
        // }
    }

    public static NativeHashMap<int2, GridDatum> GetNativeHashMap(){
        return Int2ToFloorState;
    }

}

public static class NativeHashMapFunctions
{
    public static void ChangeValue(NativeHashMap<int2, GridDatum> HashMap, int2 Key, GridDatum NewVal)
    {
        // Debug.Log(Key);
        HashMap.Remove(Key);
        HashMap.TryAdd(Key, NewVal);
    }
}

public struct GridData : IComponentData {
    public NativeHashMap<int2, GridDatum> Int2ToFloorState;
    
} 

public struct GridDatum
{
    public FloorState State { get; private set; }
    public FloorState DayState { get; private set; }
    public FloorState NightState { get; private set; }
    public Quaternion Direction {get; private set;}

    //Constructors
    public GridDatum(FloorState InitialState){
        State = InitialState;
        DayState = InitialState;
        NightState = InitialState;
        Direction = Quaternion.identity;
    }
    public GridDatum(FloorState Day, FloorState Night){
        State = Day;
        DayState = Day;
        NightState = Night;
        Direction = Quaternion.identity;
    }
    public GridDatum(FloorState Day, FloorState Night, Vector3 Dir){
        State = Day;
        DayState = Day;
        NightState = Night;
        Direction = Quaternion.LookRotation(Dir);
        // Debug.Log(Direction);
    }
    public GridDatum(FloorState DayNight, Vector3 Dir){
        State = DayNight;
        DayState = DayNight;
        NightState = DayNight;
        Direction = Quaternion.LookRotation(Dir);
        // Debug.Log(Direction);
    }
}