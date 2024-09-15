using Unity.Entities;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject prefab;
    public class Baker : Baker<SpawnerAuthoring> {
        public override void Bake(SpawnerAuthoring authoring)
        {
            
            Debug.Log(GameDataCenter._GameData._SlimeAmount);
            Debug.Log(authoring.prefab);
            Debug.Log(GameDataCenter._GameData._SlimePrefabECS);
            Debug.Log(authoring.prefab == GameDataCenter._GameData._SlimePrefabECS);
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new SpawnerConfig{
                SlimePrefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                Amount = 100,
                MinX = 1,
                MinY = 1,
                MaxX = 100,
                MaxY = 100
            });
        }
    };
}


public struct SpawnerConfig : IComponentData{
    public Entity SlimePrefab;
    public int Amount;
    public int MinX;
    public int MinY;
    public int MaxX;
    public int MaxY;

}
