using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    public float speed = 12f;
    Vector3 change;
    // Start is called before the first frame update

    private void Awake() {
        controller = GetComponent<CharacterController>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovePosition();
    }
    void UpdateMovePosition(){
        change = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
        controller.Move(change * speed * Time.deltaTime);
    }
}
