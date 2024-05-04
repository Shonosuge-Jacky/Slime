using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("SlimeAction")]
[TaskDescription("Rotate to the direction of target. Returns Success.")]
public class RotateToTargetGlobal : SlimeAction
{
    [SerializeField] protected SharedTransform targetTransform;
    private Quaternion target;
    private float rotateDirection;
    public override void OnStart()
    {
        isFinished = false;
        Vector3 dir = targetTransform.Value.position - transform.position;
        dir.Normalize();
        target = Quaternion.LookRotation(dir);
        rotateDirection = dir.x >= 0? 1f: -1f;
        // Debug.Log(Quaternion.Angle(transform.rotation, target).ToString() + dir + "  " +  target + rotateDirection);
    }

    public override TaskStatus OnUpdate()
    {
        transform.rotation *= Quaternion.AngleAxis(rotateDirection * myProperty.turnSpeed * Time.deltaTime, Vector3.up);
        isFinished = Quaternion.Angle(transform.rotation, target) <= 10;
        return isFinished? TaskStatus.Success : TaskStatus.Running;
    }
    
}
