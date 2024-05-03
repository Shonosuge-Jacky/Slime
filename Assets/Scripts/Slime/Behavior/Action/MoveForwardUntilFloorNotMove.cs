using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("SlimeAction")]
[TaskDescription("Keep Move forwards until the FloorState is not Move. Returns Success.")]
public class MoveForwardUntileFloorNotMove : SlimeAction
{
    public override TaskStatus OnUpdate()
    {
        myTransform.position += transform.forward * myProperty.moveSpeed * Time.deltaTime;
        if(!isFinished && myProperty.groundCheck.isGround){ 
            myRigidbody.velocity = new Vector3(
                myRigidbody.velocity.x, 
                myRigidbody.velocity.y+ myProperty.jumpForce + Random.Range(-2,2), 
                myRigidbody.velocity.z );
        }
        isFinished = myProperty.currGrid.floorState != FloorState.Move;
        return isFinished && myProperty.groundCheck.isGround? TaskStatus.Success : TaskStatus.Running;
    }
    public override void OnEnd()
    {
    }
}
