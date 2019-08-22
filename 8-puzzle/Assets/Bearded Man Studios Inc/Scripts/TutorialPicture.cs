using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialPicture : MonoBehaviour
{
    public GameObject MainImage;
    public GameObject LeftButtonImage;
    public GameObject RightButtonImage;
    public GameObject Page;

    public List<Sprite> sprites = new List<Sprite>();
    public List<Sprite> ButtonImage = new List<Sprite>();
    public List<Sprite> PageImage = new List<Sprite>();
    int NowPage;
    // Start is called before the first frame update
    void Start()
    {
        NowPage = 1;
        ShowPage();
    }
    public void PressKey(int nKey)
    {
        if (nKey == 1)
        {                   //lastpage
            NowPage = NowPage - 1;
            if (NowPage == 0) NowPage = 1;//GoBacktoTutorial();
            else ShowPage();
        }
        else if (nKey == 2)
        {
            NowPage = NowPage + 1;
            if (NowPage >= 6) GoBacktoMainMenu();
            else ShowPage();
        }
    }
    
    /*
    void GoBacktoTutorial()
    {
        SceneManager.LoadScene("Tutorial1");
    }
    */

    void GoBacktoMainMenu()
    {
        SceneManager.LoadScene("MultiplayerMenu");
    }

    void ShowPage()
    {
        if (NowPage == 1)
        {
            MainImage.GetComponent<Image>().sprite=sprites[0];
            LeftButtonImage.GetComponent<Image>().sprite=ButtonImage[0];
            RightButtonImage.GetComponent<Image>().sprite=ButtonImage[3];
            Page.GetComponent<Image>().sprite=PageImage[0];
        }
        else if (NowPage == 2)
        {
            MainImage.GetComponent<Image>().sprite=sprites[1];
            LeftButtonImage.GetComponent<Image>().sprite=ButtonImage[1];
            RightButtonImage.GetComponent<Image>().sprite=ButtonImage[3];
            Page.GetComponent<Image>().sprite=PageImage[1];
        }
        else if (NowPage == 3)
        {
            MainImage.GetComponent<Image>().sprite=sprites[2];
            LeftButtonImage.GetComponent<Image>().sprite=ButtonImage[1];
            RightButtonImage.GetComponent<Image>().sprite=ButtonImage[3];
            Page.GetComponent<Image>().sprite=PageImage[2];
        }
        else if (NowPage == 4)
        {
            MainImage.GetComponent<Image>().sprite=sprites[3];
            LeftButtonImage.GetComponent<Image>().sprite=ButtonImage[1];
            RightButtonImage.GetComponent<Image>().sprite=ButtonImage[3];
            Page.GetComponent<Image>().sprite=PageImage[3];
        }
        else if (NowPage == 5)
        {
            MainImage.GetComponent<Image>().sprite=sprites[4];
            LeftButtonImage.GetComponent<Image>().sprite=ButtonImage[1];
            RightButtonImage.GetComponent<Image>().sprite=ButtonImage[2];
            Page.GetComponent<Image>().sprite=PageImage[4];
        }
    }
}
