using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    CharacterController controller;
    public float speed = 12f;
    Vector3 change;
    PlayerAreaOfInterest areaOfInterest;
    // Start is called before the first frame update

    private void Awake() {
        controller = GetComponent<CharacterController>();
        areaOfInterest = transform.GetChild(0).GetComponent<PlayerAreaOfInterest>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovePosition();
        if(Input.GetKeyDown(KeyCode.C)){
            CallSlime();
        }
    }
    void UpdateMovePosition(){
        change = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
        controller.Move(change * speed * Time.deltaTime);
    }

    void CallSlime(){
        foreach(GameObject slime in areaOfInterest.GetInterestedSlimes()){
            slime.transform.parent.parent.GetComponent<SlimeAIManager>().GetCalled(transform);
        }
    }
}
