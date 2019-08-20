using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzle : MonoBehaviour
{
    public GameObject[] switches;

    private int rotNum;
    //private bool isLoaded;
    private float rotationSpeed;
    /*
    private Material indexMaterial;
    private Material scoreMaterial;
    private Transform emissiveObject0;
    private Transform emissiveObject1;
    */

    public int RotNum
    {
        get
        {
            return rotNum;
        }
    }
    
    void Start()
    {
        rotNum = 0;
        rotationSpeed = 60f;
    }

    /*
    void CheckObjects()
    {
        if (!isLoaded)
        {
            emissiveObject0 = transform.Find("ApertureSwitchEmission/SwitchEmission");
            emissiveObject1 = transform.Find("ApertureSwitchEmission/SwitchEmissionCenter");
            indexMaterial = emissiveObject1.GetComponent<Renderer>().material;
            scoreMaterial = emissiveObject0.GetComponent<Renderer>().material;
        }
    }
    */

    void Update()
    {
        if (rotNum == 0) return;

        switches[rotNum - 1].transform.Find("Switch/ApertureSwitchEmission/SwitchEmission").Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        switches[rotNum - 1].transform.Find("Switch/ApertureSwitchEmission/SwitchEmissionCenter").Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        /*
        if (isRotate)
        {
            //CheckObjects();
            emissiveObject0.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            emissiveObject1.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        */
    }

    public void SetRotNum(int rotNum)
    {
        this.rotNum = rotNum;
        Debug.Log("SetRotNum : " + rotNum);
        GameManager.instance.importantMsg.text = "SetRotNum : " + rotNum;
    }

    /*
    public void SetScoreTexture(Texture scoreTexture)
    {
        CheckObjects();
        scoreMaterial.SetTexture("_EmissionMap", scoreTexture);
    }

    public void SetIndexTexture(Texture indexTexture)
    {
        CheckObjects();
        indexMaterial.SetTexture("_EmissionMap", indexTexture);
    }
    */
}
