// using Unity.Burst;
// using Unity.Entities;
// using Unity.Mathematics;
// using Unity.Transforms;
// using UnityEngine;

// public partial struct DataCenterSystem : ISystem
// {
    
//     public void OnCreate(ref SystemState state) 
//     { 
//         // state.RequireForUpdate<SpawnerConfig>();
//     }

//     public void OnUpdate(ref SystemState state)
//     {
//         state.Enabled = false;

//         var go = GameDataCenter._GameData;

//         var entity = state.EntityManager.CreateEntity();
//         state.EntityManager.AddComponentData(entity, new SpawnerConfig
//         {
//             SlimePrefab = GameDataCenter._GameData
//         });
//     }


// }
