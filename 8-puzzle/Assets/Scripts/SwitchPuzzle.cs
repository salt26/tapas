using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzle : MonoBehaviour
{
    private bool isRotate;
    private bool isLoaded;
    private float rotationSpeed;
    private Material indexMaterial;
    private Material scoreMaterial;
    private Transform emissiveObject0;
    private Transform emissiveObject1;

    void Start()
    {
        rotationSpeed = 60f;
        isRotate = false;
        isLoaded = false;
    }

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

    void Update()
    {
        if (isRotate)
        {
            CheckObjects();
            emissiveObject0.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            emissiveObject1.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    public void SetRotation(bool isRotate)
    {
        this.isRotate = isRotate;
    }

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
}
