using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class GridDataAuthoring : MonoBehaviour
{
    public FloorGridsStorage grid;

    // private class Baker : Baker<GridDataAuthoring>
    // {
    //     public override void Bake(GridDataAuthoring authoring)
    //     {
    //         Dictionary<float3, DayNightFloorState> temp = new Dictionary<float3, DayNightFloorState>();
            
    //         Entity entity = GetEntity(TransformUsageFlags.Dynamic);
    //         AddComponent(entity, new GridData{
    //             float3ToGrid = grid;
    //         });
    //     }
    // }
}

public struct GridData : IComponentData {
    // public NativeHashMap<float2, FloorState> float2ToFloorState;
} 
