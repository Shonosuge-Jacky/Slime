using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct SlimeSystem : ISystem
{
    // public void OnCreate(ref SystemState state) { 
    //     state.RequireForUpdate<SlimeComponent>();  //make sure system OnUpdate will only run if there is at least 1 Entity with RotateSpeed Component

    // }

    // [BurstCompile]
    // public void OnUpdate(ref SystemState state) { 
    //     // state.Enabled = false;
    //     // return;
    //     GridData _GridData = SystemAPI.GetSingleton<GridData>();
    //     foreach ((RefRW<SlimeComponent> slime, RefRO<LocalTransform> localTransform) in SystemAPI.Query<RefRW<SlimeComponent>, RefRO<LocalTransform>>().WithAll<SlimeComponent>()){
    //         var slimeJob = new SlimeJob
    //         {
    //             Float2ToFloorState = _GridData.float2ToFloorState,
    //             M_SlimeComponent = slime,
    //             M_SlimeLocalTransform = localTransform
    //         };
    //         slimeJob.ScheduleParallel();
    //     }
        
    //     //For IJobFor:
    //     //  state.Dependency = rotationCubeJob.Schedule(state.Dependency);
    // }

    // [BurstCompile]
    // [WithAll(typeof(SlimeComponent))]
    // public partial struct SlimeJob: IJobEntity {
    //     public NativeHashMap<float2, FloorState> Float2ToFloorState;
    //     public RefRW<SlimeComponent> M_SlimeComponent;
    //     public RefRO<LocalTransform> M_SlimeLocalTransform;
    //     public void Execute(){
    //         UnityEngine.Debug.Log($"Curr Slime in Position: ({M_SlimeLocalTransform.ValueRO.Position.x}, {M_SlimeLocalTransform.ValueRO.Position.y}) --- ");

    //     }  
    // }
}
