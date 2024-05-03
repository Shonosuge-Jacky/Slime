using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGround; 
    private void OnCollisionEnter(Collision other) {
        isGround = true;
    }
    private void OnCollisionExit(Collision other) {
        isGround = false;
    }
}
