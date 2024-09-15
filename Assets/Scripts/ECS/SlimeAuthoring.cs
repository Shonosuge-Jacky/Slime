using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SlimeAuthoring : MonoBehaviour
{
    private class Baker : Baker<SlimeAuthoring>
    {

        public override void Bake(SlimeAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new SlimeComponent 
            { 
            //     CurrSlimeState = GameDataCenter._GameData._SlimeInitialState,
            //     MoveSpeed = GameDataCenter._GameData._SlimeMoveSpeed,
            //     TurnSpeed = GameDataCenter._GameData._SlimeTurnSpeed,
            //     JumpForce = GameDataCenter._GameData._SlimeJumpForce,
            //     CurrEmoji = GameDataCenter._GameData._SlimeInitialEmoji,
            
                // CurrSlimeState = SlimeState.Idle,
                MoveSpeed = 0,
                TurnSpeed = 0,
                JumpForce = 0,
                // CurrEmoji = Emoji.Idle,
                Timer = 0
            });
        }
    }
}

public struct SlimeComponent : IComponentData {
    // public SlimeState CurrSlimeState;
    public float MoveSpeed;
    public float TurnSpeed;
    public float JumpForce;
    // public Emoji CurrEmoji;
    public float Timer;
}
