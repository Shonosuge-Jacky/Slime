using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("SlimeAction")]
[TaskDescription("After jumping, make the root velocity = 0. Returns Success.")]
public class Stationary : SlimeAction
{
    public override void OnStart()
    {
        myRigidbody.velocity = Vector3.zero;
    }
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
