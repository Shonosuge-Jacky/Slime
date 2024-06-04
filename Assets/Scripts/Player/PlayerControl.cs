using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform;
using UnityEngine;
using DG.Tweening;

public class PlayerControl : MonoBehaviour
{
    
    [Header("Property")]
    public float speed = 12f;
    // public float jumpForce = 5;
    // public float upwardGravity = 6;
    // public float midAirGravity = 10;
    // public float fallGravity = 16;
    public bool isGrounded;
    Vector3 change;
    PlayerAreaOfInterest areaOfInterest;
    
    // Start is called before the first frame update

    private void Awake() {
        areaOfInterest = transform.GetChild(0).GetComponent<PlayerAreaOfInterest>();
    }

    void Update()
    {
        if(GameManager.Instance.isControlable){
            CheckMove();
            // isGrounded = Physics.Raycast(transform.position, -Vector3.up, 6.1f);
            if(Input.GetKeyDown(KeyCode.L)){
                CallSlime();
            }
        }
    }
    private void LateUpdate() {
        // CheckJump();
    }

    void CheckMove(){
        change = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
        transform.position += change * speed * Time.deltaTime;
    }
    // void UpdateMovePosition(){
    //     change = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
    //     controller.Move(change * speed * Time.deltaTime);
    // }

    // void CheckJump(){
    //     if(isGrounded && Input.GetKeyDown(KeyCode.Space)){
    //         GetComponent<Rigidbody>().AddForce(transform.up * jumpForce, ForceMode.Impulse);
    //     }
    // }

    

    void CallSlime(){
        foreach(GameObject slime in areaOfInterest.GetInterestedSlimes()){
            slime.transform.parent.parent.GetComponent<SlimeAIManager>().GetCalled(transform);
        }
    }
}