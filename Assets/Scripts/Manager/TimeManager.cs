using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeManager : MonoBehaviour
{
    public Light gamelight;
    public float speed;
    float xRotation;

    void Start()
    {
        xRotation = 10;
    }

    // Update is called once per frame
    void Update()
    {
        xRotation += speed;
        xRotation = Mathf.Clamp(xRotation, 0f, 180f);
        gamelight.transform.localRotation = Quaternion.Euler(xRotation, 45f, 0f);
    }
}
