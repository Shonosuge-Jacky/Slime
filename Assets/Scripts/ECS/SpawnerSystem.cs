using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct SpawnerSystem : ISystem
{
    
    public void OnCreate(ref SystemState state) 
    { 
        state.RequireForUpdate<SpawnerConfig>();
    }

    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;
        SpawnerConfig spawnerConfig = SystemAPI.GetSingleton<SpawnerConfig>();
        for(int i= 0 ; i < spawnerConfig.Amount; i++){
            Debug.Log("SpawnSlimePrefabEntity");
            Debug.Log(GameDataCenter.test);
            Entity spawnedEntity = state.EntityManager.Instantiate(spawnerConfig.SlimePrefab);
            state.EntityManager.AddComponent<SlimeComponent>(spawnedEntity);
            SystemAPI.SetComponent(spawnedEntity, new LocalTransform{
                Position = new float3(UnityEngine.Random.Range(spawnerConfig.MinX, spawnerConfig.MaxX), 0, UnityEngine.Random.Range(spawnerConfig.MinY, spawnerConfig.MaxY)),
                Rotation = quaternion.identity,
                Scale = 1f
            });
        }
    }


}
