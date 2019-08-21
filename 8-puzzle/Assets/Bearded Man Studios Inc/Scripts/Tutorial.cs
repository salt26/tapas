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
    public List<Sprite> BatterySprites = new List<Sprite>();

    public Image ToolTipTutorial;
    public Image BatteryImage;
    public Text TutorialLogText;

    /*
    public Text TutorialLogText1;
    public Text TutorialLogText2;
    public Text TutorialLogText3;
    public Text TutorialLogText4;
    */

    public Image SuccessImage;
    public Image TutorialInstructionImage;
    public GameObject ExitTutorialInstructionButton;

    public float fadeDuration = 1f;
    float m_Timer;
    public float displayImageDuration = 1f;
    bool m_IsSuccess=false;

    int nKey;
    int Button15=0;
    int Button24=0;
    int Button38=0;
    int Button67=0;
    int TotalButtonOn=0;
    int LastClickedButton=0;
    float speed=1.0f;
    float Timer;
    bool TimerWorks;
    public bool ButtontoTutorialOn;
    string LogText;
    bool FirstLog;
    /*
    string LogText1="";
    string LogText2="";
    string LogText3="";
    string LogText4="";
    */

    char TempChar;
    string TempString;
    int NumberofTest=0;
    // Start is called before the first frame update


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
        BatteryImage.GetComponent<Image>().sprite=BatterySprites[0];
        LogText="";
        SuccessImage.enabled=false;
        ButtontoTutorialOn=false;
        ToolTipTutorial.enabled=false;
        TimerWorks=false;
        TutorialInstructionImage.enabled=false;
        ExitTutorialInstructionButton.SetActive(false);
        FirstLog=true;
//        ButtontoTutorialPictures.GetComponent<Button>().interactable=false;
 /*       Right1.SetActive(false);
        Right2.SetActive(false);
        Right3.SetActive(false);
        Right4.SetActive(false);        */
    }
    void Update() 
    {
        if(TimerWorks) {
            Timer+=Time.deltaTime;
            if(Timer>2) {
                TimerWorks=false;
                ToolTipTutorial.enabled=false;
            }
        }
        if(m_IsSuccess==true) {
            SuccessImage.enabled=true;
            EndTutorial(SuccessImage);
        } else {
            if(LastClickedButton!=0) {
                if(LastClickedButton==1) {
                    //transform.rotation=Quaternion.identity;
                    Button1.transform.Rotate(Vector3.forward*speed);
                }
                if(LastClickedButton==2) {
                    //transform.rotation=Quaternion.identity;
                    Button2.transform.Rotate(Vector3.forward*speed);
                }
                if(LastClickedButton==3) {
                    //transform.rotation=Quaternion.identity;
                    Button3.transform.Rotate(Vector3.forward*speed);
                }
                if(LastClickedButton==4) {
                    //transform.rotation=Quaternion.identity;
                    Button4.transform.Rotate(Vector3.forward*speed);
                }
                if(LastClickedButton==5) {
                    //transform.rotation=Quaternion.identity;
                    Button5.transform.Rotate(Vector3.forward*speed);
                }
                if(LastClickedButton==6) {
                    //transform.rotation=Quaternion.identity;
                    Button6.transform.Rotate(Vector3.forward*speed);
                }
                if(LastClickedButton==7) {
                    //transform.rotation=Quaternion.identity;
                    Button7.transform.Rotate(Vector3.forward*speed);
                }
                if(LastClickedButton==8) {
                    //transform.rotation=Quaternion.identity;
                    Button8.transform.Rotate(Vector3.forward*speed);
                }
            }
        }
    }
    // Update is called once per frame
    public void PressKey(int nKey)
    {
        if(nKey!=LastClickedButton) {
            switch (nKey) 
            {
                case 1:
                    if(Button15==0) Button15=1;
                    else Button15=0;
                    init_ButtonRotation();
                    LastClickedButton=1;
                    break;
                case 2:
                    if(Button24==0) Button24=1;
                    else Button24=0;
                    init_ButtonRotation();
                    LastClickedButton=2;
                    break;
                case 3:
                    if(Button38==0) Button38=1;
                    else Button38=0;
                    init_ButtonRotation();
                    LastClickedButton=3;
                    break;
                case 4:
                    if(Button24==0) Button24=1;
                    else Button24=0;
                    init_ButtonRotation();
                    LastClickedButton=4;
                    break;
                case 5:
                    if(Button15==0) Button15=1;
                    else Button15=0;
                    init_ButtonRotation();
                    LastClickedButton=5;
                    break;
                case 6:
                    if(Button67==0) Button67=1;
                    else Button67=0;
                    init_ButtonRotation();
                    LastClickedButton=6;
                    break;
                case 7:
                    if(Button67==0) Button67=1;
                    else Button67=0;
                    init_ButtonRotation();
                    LastClickedButton=7;
                    break;
                case 8:
                    if(Button38==0) Button38=1;
                    else Button38=0;
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
                    QuitTutorialInstruction();
                    break;
                case 12:
                    ResetTutorial();
                    break;
            }
            if(nKey<=8) {
                TotalButtonOn=Button15+Button24+Button38+Button67;
                BatteryImage.GetComponent<Image>().sprite=BatterySprites[TotalButtonOn];
                ShowLog();
                if(TotalButtonOn==4) {
                    m_IsSuccess=true;
                    ButtontoTutorialOn=true;
                }
            }
        }
    }

    void ShowLog() 
    {
        TempChar=(char)(LastClickedButton+'0');
        TempString=TempChar.ToString();
        if(!FirstLog) {
            LogText=LogText+"\n ";
        } else {
            LogText=LogText+" ";
            FirstLog=false;
        }
        if(TotalButtonOn==0) {
            LogText=LogText+TempString+" 0/4";
        } else if(TotalButtonOn==1) {
            LogText=LogText+TempString+" 1/4";
        } else if(TotalButtonOn==2) {
            LogText=LogText+TempString+" 2/4";
        } else if(TotalButtonOn==3) {
            LogText=LogText+TempString+" 3/4";
        } else if(TotalButtonOn==4) {
            LogText=LogText+TempString+" 4/4";
        }
        Debug.Log(LogText);
        TutorialLogText.GetComponent<Text>().text=LogText;

    }
    void ResetTutorial() 
    {
        init_ButtonRotation();
        Button15=0;
        Button24=0;
        Button67=0;
        Button38=0;
        TotalButtonOn=0;
        LastClickedButton=0;
        BatteryImage.GetComponent<Image>().sprite=BatterySprites[TotalButtonOn];
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
            SuccessImage.enabled=false;
        }
    }

    void ChangeScenetoTutorialPicture() {
        if(ButtontoTutorialOn) {                                    //이거 public으로 하면 못받나???
            SceneManager.LoadScene("TutorialPicture");
        } else {
            ShowToolTip();
        }
    }
    void ShowToolTip() {
        ToolTipTutorial.enabled=true;
        Timer=0;
        TimerWorks=true;
    }
    void ShowTutorialInstruction() {
        TutorialInstructionImage.enabled=true;
        ExitTutorialInstructionButton.SetActive(true);
    }
    void QuitTutorialInstruction() {
        TutorialInstructionImage.enabled=false;
        ExitTutorialInstructionButton.SetActive(false);
    }
}

    /*
    void PrintLog() {
        NumberofTest++;
        int divi=NumberofTest/11;
        if(divi==0) {
            if(TotalButtonOn==0) {
                LogText1=LogText1+"\n"+TempString+"   0/4";
            } else if(TotalButtonOn==1) {
                LogText1=LogText1+"\n"+TempString+"   1/4";
            } else if(TotalButtonOn==2) {
                LogText1=LogText1+"\n"+TempString+"   2/4";
            } else if(TotalButtonOn==3) {
                LogText1=LogText1+"\n"+TempString+"   3/4";
            } else if(TotalButtonOn==4) {
                LogText1=LogText1+"\n"+TempString+"   4/4";
            }
            TutorialLogText1.GetComponent<Text>().text=LogText1;
        } else if(divi==1) {
            if(TotalButtonOn==0) {
                LogText2=LogText2+"\n"+TempString+"   0/4";
            } else if(TotalButtonOn==1) {
                LogText2=LogText2+"\n"+TempString+"   1/4";
            } else if(TotalButtonOn==2) {
                LogText2=LogText2+"\n"+TempString+"   2/4";
            } else if(TotalButtonOn==3) {
                LogText2=LogText2+"\n"+TempString+"   3/4";
            } else if(TotalButtonOn==4) {
                LogText2=LogText2+"\n"+TempString+"   4/4";
            }
            TutorialLogText2.GetComponent<Text>().text=LogText2;
        } else if(divi==2) {
            if(TotalButtonOn==0) {
                LogText3=LogText3+"\n"+TempString+"   0/4";
            } else if(TotalButtonOn==1) {
                LogText3=LogText3+"\n"+TempString+"   1/4";
            } else if(TotalButtonOn==2) {
                LogText3=LogText3+"\n"+TempString+"   2/4";
            } else if(TotalButtonOn==3) {
                LogText3=LogText3+"\n"+TempString+"   3/4";
            } else if(TotalButtonOn==4) {
                LogText3=LogText3+"\n"+TempString+"   4/4";
            }
            TutorialLogText3.GetComponent<Text>().text=LogText3;
        } else if(divi==3) {
            if(TotalButtonOn==0) {
                LogText4=LogText4+"\n"+TempString+"   0/4";
            } else if(TotalButtonOn==1) {
                LogText4=LogText4+"\n"+TempString+"   1/4";
            } else if(TotalButtonOn==2) {
                LogText4=LogText4+"\n"+TempString+"   2/4";
            } else if(TotalButtonOn==3) {
                LogText4=LogText4+"\n"+TempString+"   3/4";
            } else if(TotalButtonOn==4) {
                LogText4=LogText4+"\n"+TempString+"   4/4";
            }
            TutorialLogText4.GetComponent<Text>().text=LogText4;
        }
    }
    */

    /*
    Right1.SetActive(false);
    Right2.SetActive(false);
    Right3.SetActive(false);
    Right4.SetActive(false);
    switch (TotalButtonOn) {
        case 4:
            Right4.SetActive(true);
            Right3.SetActive(true);
            Right2.SetActive(true);
            Right1.SetActive(true);
            break;
        case 3:
            Right3.SetActive(true);
            Right2.SetActive(true);
            Right1.SetActive(true);
            break;
        case 2:
            Right2.SetActive(true);
            Right1.SetActive(true);
            break;
        case 1:
            Right1.SetActive(true);
            break;
    }
    }*/
/*
    void CastRay() 
    {
        target=null;
        Vector2 pos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit=Physics2D.Raycast(pos,Vector2.zero,0f);

        if(hit.collider !=null) {
            target=hit.collider.gameObject;
        }
    }
    private GameObject GetClickedobject() 
    {
        RaycastHit hit;
        GameObject target=null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        if( true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit))) 
        {
            target = hit.collider.gameObject; 
        }
        return target;
    }*/

