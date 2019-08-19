using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameManager : MonoBehaviour
{
    // Update is called once per frame
    public void pressKey(int nKey) {
        if(nKey==1) {
            Application.Quit();
        }
    }
}
