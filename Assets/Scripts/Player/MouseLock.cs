using UnityEngine;
using DG.Tweening;

public class MouseLock : MonoBehaviour
{
    [Header("Property")]
    public float standingY = 0;
    public float crouchingY = -4;
    public float minFieldOfView = 40;
    public float maxFieldOfView = 80;
    public float sensitivity = 100f;
    float xRotation = 0f;
    public float newFieldOfView = 50;
    [Header("Variable")]
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCursorRotation();
        CheckCrouch();
        CheckZoom();
    }

    void UpdateCursorRotation(){
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerTransform.Rotate(Vector3.up * mouseX);
    }

    void CheckCrouch(){
        if(Input.GetKey(KeyCode.LeftShift)){
            transform.DOLocalMoveY(crouchingY, 0.7f);
        }else{
            transform.DOLocalMoveY(standingY, 0.7f);
        }
    }
    void CheckZoom(){
        
        float fieldOfViewChange = newFieldOfView + Input.mouseScrollDelta.y * -5;
        
        if(minFieldOfView <= fieldOfViewChange && maxFieldOfView >= fieldOfViewChange){
            // Debug.Log(fieldOfViewChange);
            newFieldOfView = fieldOfViewChange;
        }
        GetComponent<Camera>().DOFieldOfView(newFieldOfView, 0.3f);
    }

}
