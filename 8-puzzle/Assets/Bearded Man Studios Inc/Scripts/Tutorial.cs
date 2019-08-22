using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    public GameObject Button4;
    public GameObject Button5;
    public GameObject Button6;
    public GameObject Button7;
    public GameObject Button8;
    public GameObject ButtontoTutorialPictures;
    public GameObject ResetButton;
    public List<Sprite> sprites = new List<Sprite>();
    public List<Sprite> Doorsprites = new List<Sprite>();
    public List<Sprite> Numbersprites = new List<Sprite>();
    public List<Sprite> Tutorialsprites = new List<Sprite>();
    public Image SuccessImage;
    public Image ButtonBackground;
    public Image NumberofGroup;
    public Text ShowLogText;
    public Image TutorialInstrction;
    public GameObject TutorialInstrctionNextButton;
    public GameObject TutorialInstrctionBeforeButton;
    
    public float fadeDuration = 1f;
    float m_Timer;
    public float displayImageDuration = 1f;
    bool m_IsSuccess=false;

    int nKey;
    int Button12=0;
    int Button36=0;
    int Button48=0;
    int Button57=0;
    int TotalButtonOn=0;
    int LastClickedButton=0;
    float speed=1.0f;
    float Timer;
    bool TimerWorks;
    int i;
    string LogText="";
    bool succeed;
    List<string> LogList = new List<string>();

    char TempChar;
    string TempString;
    int TutorialInstrctionPage;

    void Start()
    {
        Button1.GetComponent<Image>().sprite=sprites[4];
        Button2.GetComponent<Image>().sprite=sprites[4];
        Button3.GetComponent<Image>().sprite=sprites[4];
        Button4.GetComponent<Image>().sprite=sprites[4];
        Button5.GetComponent<Image>().sprite=sprites[4];
        Button6.GetComponent<Image>().sprite=sprites[4];
        Button7.GetComponent<Image>().sprite=sprites[4];
        Button8.GetComponent<Image>().sprite=sprites[4];
        SuccessImage.enabled=false;
        ButtontoTutorialPictures.SetActive(false);
        TimerWorks=false;
        ButtonBackground.GetComponent<Image>().sprite=Doorsprites[0];
        NumberofGroup.GetComponent<Image>().sprite=Numbersprites[0];
        for(i=0;i<13;i++) {
            LogList.Add("\n");
            LogText=LogText+LogList[i];
        }
        ShowLogText.GetComponent<Text>().text=LogText;
        TutorialInstrctionNextButton.SetActive(false);
        TutorialInstrctionBeforeButton.SetActive(false);
        TutorialInstrction.enabled=false;
        succeed=false;
    }
    void Update() 
    {
        if(TimerWorks) {
            Timer+=Time.deltaTime;
            if(Timer>2) {
                TimerWorks=false;
            }
        }
        //Debug.Log(m_IsSuccess);
        if(m_IsSuccess==true) {
//            Debug.Log("suc");
            SuccessImage.enabled=true;
            Timer=0f;
            TimerWorks=true;
            EndTutorial(SuccessImage);
        } else {
            if(LastClickedButton!=0) {
                if(LastClickedButton==1) {
                    Button1.transform.Rotate(Vector3.forward*speed);
                }
                if(LastClickedButton==2) {
                    Button2.transform.Rotate(Vector3.forward*speed);
                }
                if(LastClickedButton==3) {
                    Button3.transform.Rotate(Vector3.forward*speed);
                }
                if(LastClickedButton==4) {
                    Button4.transform.Rotate(Vector3.forward*speed);
                }
                if(LastClickedButton==5) {
                    Button5.transform.Rotate(Vector3.forward*speed);
                }
                if(LastClickedButton==6) {
                    Button6.transform.Rotate(Vector3.forward*speed);
                }
                if(LastClickedButton==7) {
                    Button7.transform.Rotate(Vector3.forward*speed);
                }
                if(LastClickedButton==8) {
                    Button8.transform.Rotate(Vector3.forward*speed);
                }
            }
        }
    }

    public void PressKey(int nKey)
    {
        if(succeed) {
            if(nKey<=8) return;
        }
        if(nKey!=LastClickedButton) {
            switch (nKey) 
            {
                case 1:
                    if(Button12==0) Button12=1;
                    else Button12=0;
                    init_ButtonRotation();
                    LastClickedButton=1;
                    break;
                case 2:
                    if(Button12==0) Button12=1;
                    else Button12=0;
                    init_ButtonRotation();
                    LastClickedButton=2;
                    break;
                case 3:
                    if(Button36==0) Button36=1;
                    else Button36=0;
                    init_ButtonRotation();
                    LastClickedButton=3;
                    break;
                case 4:
                    if(Button48==0) Button48=1;
                    else Button48=0;
                    init_ButtonRotation();
                    LastClickedButton=4;
                    break;
                case 5:
                    if(Button57==0) Button57=1;
                    else Button57=0;
                    init_ButtonRotation();
                    LastClickedButton=5;
                    break;
                case 6:
                    if(Button36==0) Button36=1;
                    else Button36=0;
                    init_ButtonRotation();
                    LastClickedButton=6;
                    break;
                case 7:
                    if(Button57==0) Button57=1;
                    else Button57=0;
                    init_ButtonRotation();
                    LastClickedButton=7;
                    break;
                case 8:
                    if(Button48==0) Button48=1;
                    else Button48=0;
                    init_ButtonRotation();
                    LastClickedButton=8;
                    break;
                case 9:
                    ChangeScenetoTutorialPicture();
                    break;
                case 10:
                    ShowTutorialInstruction();
                    break;
                case 11:
                    NextTutorialInstruction();
                    break;
                case 12:
                    ResetTutorial();
                    break;
                case 13:
                    BeforeTutorialInstruction();
                    break;
            }
            if(nKey<=8) {
                TotalButtonOn=Button12+Button48+Button36+Button57;
                for(i=0;i<12;i++) {
                    LogList[i]=LogList[i+1];
                }
                TempChar=(char)(nKey+'0');
                TempString=TempChar.ToString();
                if(TotalButtonOn==0) {
                    LogList[12]=TempString+"번 스위치 -> 그룹 0개 켜짐\n";
                    NumberofGroup.GetComponent<Image>().sprite=Numbersprites[0];
                }
                if(TotalButtonOn==1) {
                    LogList[12]=TempString+"번 스위치 -> 그룹 1개 켜짐\n";
                    NumberofGroup.GetComponent<Image>().sprite=Numbersprites[1];
                }
                if(TotalButtonOn==2) {
                    LogList[12]=TempString+"번 스위치 -> 그룹 2개 켜짐\n";
                    NumberofGroup.GetComponent<Image>().sprite=Numbersprites[2];
                }
                if(TotalButtonOn==3) {
                    LogList[12]=TempString+"번 스위치 -> 그룹 3개 켜짐\n";
                    NumberofGroup.GetComponent<Image>().sprite=Numbersprites[3];
                }
                if(TotalButtonOn==4) {
                    m_IsSuccess=true;
                    LogList[12]=TempString+"번 스위치 -> 그룹 4개 켜짐";
                    NumberofGroup.GetComponent<Image>().sprite=Numbersprites[4];
                    ButtonBackground.GetComponent<Image>().sprite=Doorsprites[1];
                    LastClickedButton=0;
                    init_ButtonRotation();
                    ButtontoTutorialPictures.SetActive(true);
                    succeed=true;
                }
                ShowLog();
            }
        }
    }
    void ResetTutorial() 
    {
        init_ButtonRotation();
        Button12=0;
        Button48=0;
        Button36=0;
        Button57=0;
        TotalButtonOn=0;
        LastClickedButton=0;
        ButtonBackground.GetComponent<Image>().sprite=Doorsprites[0];
        LogText="";
        for(i=0;i<13;i++) {
            LogList[i]="\n";
        }
        ShowLog();
        succeed=false;
        NumberofGroup.GetComponent<Image>().sprite=Numbersprites[TotalButtonOn];
    }

    void ShowLog()
    {
        LogText="";
        for(i=0;i<13;i++) {
            LogText+=LogList[i];
        }
        ShowLogText.GetComponent<Text>().text=LogText;
    }

    void init_ButtonRotation()
    {
        switch(LastClickedButton) {
            case 1:
                Button1.transform.rotation = Quaternion.identity;
                break;
            case 2:
                Button2.transform.rotation = Quaternion.identity;
                break;
            case 3:
                Button3.transform.rotation = Quaternion.identity;
                break;
            case 4:
                Button4.transform.rotation = Quaternion.identity;
                break;
            case 5:
                Button5.transform.rotation = Quaternion.identity;
                break;
            case 6:
                Button6.transform.rotation = Quaternion.identity;
                break;
            case 7:
                Button7.transform.rotation = Quaternion.identity;
                break;
            case 8:
                Button8.transform.rotation = Quaternion.identity;
                break;

        }
    }
    void EndTutorial(Image image) {
        m_Timer += Time.deltaTime;
        image.color = new Color(image.color.r,image.color.g,image.color.b, m_Timer / fadeDuration);
        if(m_Timer>fadeDuration + displayImageDuration) {
            m_IsSuccess=false;
            TimerWorks=false;
            SuccessImage.enabled=false;
        }
    }

    void ChangeScenetoTutorialPicture() {
        SceneManager.LoadScene("TutorialPicture");
    }
    void ShowTutorialInstruction() {
        TutorialInstrctionNextButton.SetActive(true);
        TutorialInstrctionBeforeButton.SetActive(true);
        TutorialInstrction.enabled=true;
        TutorialInstrctionPage=0;
        TutorialInstrction.GetComponent<Image>().sprite=Tutorialsprites[TutorialInstrctionPage];
    }
    void NextTutorialInstruction() {
        TutorialInstrctionPage++;
        if(TutorialInstrctionPage>=0 && TutorialInstrctionPage<=3) {
            TutorialInstrction.GetComponent<Image>().sprite=Tutorialsprites[TutorialInstrctionPage];
        } else if(TutorialInstrctionPage==4) {
            ExitTutorialInstruction();
        }
    }
    void BeforeTutorialInstruction() {
        TutorialInstrctionPage--;
        if(TutorialInstrctionPage>=0 && TutorialInstrctionPage<=3) {
            TutorialInstrction.GetComponent<Image>().sprite=Tutorialsprites[TutorialInstrctionPage];
        } else if(TutorialInstrctionPage==-1) {
            ExitTutorialInstruction();
        }
    }
    void ExitTutorialInstruction() {
        TutorialInstrctionNextButton.SetActive(false);
        TutorialInstrctionBeforeButton.SetActive(false);
        TutorialInstrction.enabled=false;
    }
}