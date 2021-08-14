using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    [SerializeField] private float skyboxRotationSpeed = 1f;
    private Material skyboxMaterial;

    private void Start()
    {
        skyboxMaterial = GetSkyboxMaterial();
        skyboxMaterial = Instantiate(skyboxMaterial);
        SetSkyboxMaterial(skyboxMaterial);
    }

    private void Update()
    {
        skyboxMaterial.SetFloat("_Rotation", Time.time * skyboxRotationSpeed);
    }

    private Material GetSkyboxMaterial()
    {
        Skybox skybox = GetComponent<Skybox>();
        if (skybox != null)
        {
            return skybox.material;
        }
        else
        {
            return RenderSettings.skybox;
        }
    }

    private void SetSkyboxMaterial(Material skyboxMaterial)
    {
        Skybox skybox = GetComponent<Skybox>();
        if (skybox != null)
        {
            skybox.material = skyboxMaterial;
        }
        else
        {
            RenderSettings.skybox = skyboxMaterial;
        }
    }
}
