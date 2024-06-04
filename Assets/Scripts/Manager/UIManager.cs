using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject blackBackground;
    bool isUIAnimation;
    [Header("Setting")]
    public GameObject settingObject;
    public TMP_Text gameSpeedText;
    

    [Header("Clock")]
    public TMP_Text clock;

    [Header("Slime Information")]
    public RectTransform infoPanel;
    public TMP_Text stateText;
    [SerializeField] bool isPointing;
    public void SetPointing(bool isPointing){
        this.isPointing = isPointing;
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        UpdateSlimeInfoPanelLocation();
        if(!isUIAnimation){

        }
        CheckSettingPanel();
    }

    public void UpdateSlimeInfoPanelLocation(){
        if(isPointing){
            infoPanel.DOAnchorPosX(350, 0.5f);
        }else{
            infoPanel.DOAnchorPosX(650, 0.5f);
        }
    }
    public void UpdateSlimeInfoPanelState(SlimeState slimeState){
        stateText.text = "State: " + slimeState.ToString();
    }

    public void UpdateGameSpeedUIText(Slider slider){
        gameSpeedText.text = "Speed: " + slider.value.ToString();
    }

    public void CheckSettingPanel(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(settingObject.activeSelf){
                blackBackground.GetComponent<Image>().DOFade(0, 0.5f);
                settingObject.GetComponent<RectTransform>().DOAnchorPosX(960, 0.5f);
                DOVirtual.DelayedCall(0.5f, ()=>{
                    settingObject.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    GameManager.Instance.isControlable = true;
                    blackBackground.SetActive(false);
                });
                isUIAnimation = true;
                DOVirtual.DelayedCall(0.5f, ()=> isUIAnimation = false);
                
                
            }else{
                settingObject.SetActive(true);
                settingObject.GetComponent<RectTransform>().position = new Vector2(960, 300);
                settingObject.GetComponent<RectTransform>().DOAnchorPosX(0, 0.5f).SetEase(Ease.OutBounce);
                
                Cursor.lockState = CursorLockMode.None;
                GameManager.Instance.isControlable = false;
                blackBackground.SetActive(true);
                blackBackground.GetComponent<Image>().DOFade(0.5f, 0.5f);

                isUIAnimation = true;
                DOVirtual.DelayedCall(0.5f, ()=> isUIAnimation = false);
            }
        }
    }   
    
    public void OpenPersonalWebsite(){
        Application.OpenURL("http://www.shonosuge.com/");
    } 
    
}
