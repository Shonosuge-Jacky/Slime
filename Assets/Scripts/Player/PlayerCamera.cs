using UnityEngine;
using DG.Tweening;

public class PlayerCamera : MonoBehaviour
{
    [Header("Property")]
    public float standingY = 0;
    public float crouchingY = -4;
    public float minFieldOfView = 40;
    public float maxFieldOfView = 80;
    public float sensitivity = 100f;
    float xRotation = 0f;
    public float newFieldOfView = 50;
    public float moveSpeed = 5f;
    [Header("Variable")]
    public Transform playerTransform;

    [Header("Raycast")]
    [SerializeField] RaycastHit HitInfo;
    public LayerMask mask;
    public float spectateDistance;
    // Start is called before the first frame update
    void Start()
    {
        // EventCenter.Instance.AddEventListener(EventType.ChangeGameModeToInspect, ()=>{gameObject.transform.eulerAngles = new Vector3(180,0,0);});
        // EventCenter.Instance.AddEventListener(EventType.ChangeGameModeToExplore, ()=>{gameObject.transform.eulerAngles = new Vector3(0,0,0);});
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isControlable){
            UpdateCursorRotation();
            if(GameManager.Instance.CurrGameMode == GameMode.Explore)
            {
                CheckCrouch();
                CheckInteractRaycast();
            }
            CheckZoom();
        }
        
        
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
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            transform.DOLocalMoveY(crouchingY, 0.5f);
        }else if(Input.GetKeyUp(KeyCode.LeftShift)){
            transform.DOLocalMoveY(standingY, 0.5f);
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

    void CheckInteractRaycast(){
        Debug.DrawRay(transform.position, transform.forward * 100.0f, Color.yellow);
        if(Physics.Raycast(transform.position,transform.forward, out HitInfo, spectateDistance, mask)){
            GameManager.Instance.UIManager.UpdateSlimeInfoPanelState(HitInfo.transform.parent.parent.GetComponent<SlimeProperty>().slimeState);
            GameManager.Instance.UIManager.SetPointing(true);
        }else{
            GameManager.Instance.UIManager.SetPointing(false);
        }
            
    }

}
